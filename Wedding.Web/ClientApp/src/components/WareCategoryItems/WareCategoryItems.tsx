import {
    Card,
    Grid,
    Box,
    CardContent,
    CardMedia,
    Typography,
    LinearProgress,
    Tooltip,
    Slider,
    CircularProgress, OutlinedInput
} from "@mui/material";
import {useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import {CategoryItemsResponseType, useLazyGetCategoryItemsQuery} from "core/api/wareCategoryApi";
import {WareCategoryItem} from "components/WareCategoryItems/WareCategoryItem/WareCategoryItem";
import {useDebounce} from "use-debounce";
import { PaginatedItems } from "components/Pagination/Pagination";
import {RangeFrom} from "core/helpers/arrayHelper";

const ELEMENTS_ON_PAGE = 5;

const valuePriceText = (value: number) => {
    return `${value} p.`;
}



export const WareCategoryItems = () => {
    const [currentPage, setCurrentPage] = useState(0)
    const [activeItems, setActiveItems] = useState<CategoryItemsResponseType>();
    const [priceValue, setPriceValue] = useState<number[]>([1, 1000]);
    const [nameValue, setNameValue] = useState<string>('')

    const [activePriceFilter, setActivePriceFilter] = useState<number[] | null>(null);
    const [activeNameFilter, setActiveNameFilter] = useState<string>('');

    const handlePriceChange = (event: Event, newValue: number | number[]) => {
        setPriceValue(newValue as number[]);
    };

    const debouncedPriceValue = useDebounce(priceValue, 300);
    const debouncedNameValue = useDebounce(nameValue, 300);

    const {id} = useParams()

    const priceMarks = [1, 1000].map(t => {return {value: t, label: valuePriceText(t)}})

    const [
        triggerCategoryItemsQuery,
        {data: itemsResponse, isFetching: isFetchingItems}
    ] = useLazyGetCategoryItemsQuery()

    useEffect(() => {
        setActiveItems(itemsResponse)
    }, [itemsResponse])

    useEffect(() => {
        setActivePriceFilter(debouncedPriceValue[0])
    }, [debouncedPriceValue, priceValue])

    useEffect(() => {
        setActiveNameFilter(debouncedNameValue[0])
    }, [debouncedNameValue, nameValue])

    useEffect(() => {
        if (activePriceFilter != null) {
            triggerCategoryItemsQuery({
                skip: currentPage * ELEMENTS_ON_PAGE,
                take: ELEMENTS_ON_PAGE,
                categoryId: id ?? '',
                search: activeNameFilter.trim() === '' ? null : activeNameFilter,
                priceFrom: activePriceFilter[0],
                priceTo: activePriceFilter[1]
            })
        }

    }, [currentPage, activePriceFilter, activeNameFilter])

    return (
        <>
        {isFetchingItems && <Grid position='absolute'><CircularProgress/></Grid>}
            <Grid container ml='50px' pr='50px' mt='5px'>
                <Grid item xs={7}>
                    {activeItems && activeItems.items.map(i => (
                        <WareCategoryItem id={i.id} name={i.name} description={i.description} price={i.price} discounted={i.discounted}/>
                    ))}
                </Grid>
                <Grid item xs={4} ml='15px' p='25px' sx={{background: 'white'}}>
                    <Grid>
                        <Typography>От {valuePriceText(priceValue[0])} до {valuePriceText(priceValue[1])}</Typography>
                    </Grid>
                    <Slider
                        getAriaLabel={() => 'Цена'}
                        value={priceValue}
                        onChange={handlePriceChange}
                        valueLabelDisplay="auto"
                        getAriaValueText={valuePriceText}
                        marks={priceMarks}
                        max={1000}
                        min={1}
                    />
                    <OutlinedInput placeholder={'Наименование'} fullWidth value={nameValue} onChange={e => setNameValue(e.target.value)}/>
                    <PaginatedItems onChangePage={setCurrentPage} itemsPerPage={5} items={RangeFrom(0, activeItems?.total ?? 0)}/>
                </Grid>
            </Grid>
        </>
    )
}