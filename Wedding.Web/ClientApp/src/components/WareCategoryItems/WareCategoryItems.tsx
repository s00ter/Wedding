import {
    Grid,
    Typography,
    Slider,
    CircularProgress, OutlinedInput, FormControl, FormLabel, RadioGroup, FormControlLabel, Radio
} from "@mui/material";
import {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import {
    CategoryItemsResponseType,
    useGetCategoryRangesQuery,
    useLazyGetCategoryItemsQuery
} from "core/api/wareCategoryApi";
import {WareCategoryItem} from "components/WareCategoryItems/WareCategoryItem/WareCategoryItem";
import {useDebounce} from "use-debounce";
import {PaginatedItems} from "components/Pagination/Pagination";
import {RangeFrom} from "core/helpers/arrayHelper";
import {ArrowBack} from "@mui/icons-material";

const ELEMENTS_ON_PAGE = 5;

const valuePriceText = (value: number) => {
    return `${value} p.`;
}


export const WareCategoryItems = () => {
    const [currentPage, setCurrentPage] = useState(0)
    const [activeItems, setActiveItems] = useState<CategoryItemsResponseType>();
    const [priceValue, setPriceValue] = useState<number[]>([0, 0]);
    const [nameValue, setNameValue] = useState<string>('')

    const [priceSortFilter, setPriceSortFilter] = useState<'asc' | 'desc'>('desc')
    const [activePriceFilter, setActivePriceFilter] = useState<number[] | null>(null);
    const [activeNameFilter, setActiveNameFilter] = useState<string>('');

    const handlePriceChange = (event: Event, newValue: number | number[]) => {
        setPriceValue(newValue as number[]);
    };

    const debouncedPriceValue = useDebounce(priceValue, 300);
    const debouncedNameValue = useDebounce(nameValue, 300);

    const navigate = useNavigate();

    const {id} = useParams()

    const [
        triggerCategoryItemsQuery,
        {data: itemsResponse, isFetching: isFetchingItems}
    ] = useLazyGetCategoryItemsQuery()

    const {data: priceRangesResponse, isFetching: isFetchingPriceRanges} = useGetCategoryRangesQuery(id ?? '')

    useEffect(() => {
        if (priceRangesResponse) {
            setPriceValue([priceRangesResponse.min, priceRangesResponse.max])
        }

    }, [priceRangesResponse])

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
                priceTo: activePriceFilter[1],
                priceDesc: priceSortFilter === 'desc'
            })
        }

    }, [currentPage, activePriceFilter, activeNameFilter, priceSortFilter])

    return (
        <>
            <Grid position={'absolute'} sx={{cursor: 'pointer'}} onClick={() => navigate('/')}><ArrowBack/></Grid>
            {(isFetchingItems || isFetchingPriceRanges) && <Grid position='absolute'><CircularProgress/></Grid>}
            <Grid container ml='50px' pr='50px' mt='5px'>
                <Grid item xs={6}>
                    {activeItems && activeItems.items.map(i => (
                        <WareCategoryItem id={i.id} name={i.name} description={i.description} price={i.price}
                                          discounted={i.discounted}/>
                    ))}
                </Grid>
                <Grid item xs={4} ml='15px' p='25px' sx={{background: 'white', position: 'relative'}}>
                    <Grid>
                        <Typography>От {valuePriceText(priceValue[0])} до {valuePriceText(priceValue[1])}</Typography>
                    </Grid>
                    {priceRangesResponse &&
                        <Slider
                            getAriaLabel={() => 'Цена'}
                            value={priceValue}
                            onChange={handlePriceChange}
                            valueLabelDisplay="auto"
                            getAriaValueText={valuePriceText}
                            marks={[
                                {value: priceRangesResponse.min, label: valuePriceText(priceRangesResponse.min)},
                                {value: priceRangesResponse.max, label: valuePriceText(priceRangesResponse.max)}
                            ]}
                            max={priceRangesResponse.max}
                            min={priceRangesResponse.min}
                        />}
                    <FormControl>
                        <RadioGroup
                            aria-labelledby="demo-radio-buttons-group-label"
                            defaultValue={priceSortFilter}
                            name="radio-buttons-group"
                            onChange={(e) => {
                                e.target.value === 'asc' ? setPriceSortFilter('asc') : setPriceSortFilter('desc')
                            }}
                        >
                            <FormControlLabel value="desc" control={<Radio/>} label="Сначала дорогие"/>
                            <FormControlLabel value="asc" control={<Radio/>} label="Сначала дешевые"/>
                        </RadioGroup>
                    </FormControl>
                    <OutlinedInput placeholder={'Наименование'} fullWidth value={nameValue}
                                   onChange={e => setNameValue(e.target.value)}/>
                    <Grid item sx={{position: 'absolute', bottom: 10}}>
                        <PaginatedItems onChangePage={setCurrentPage} itemsPerPage={5}
                                        items={RangeFrom(0, activeItems?.total ?? 0)}/>
                    </Grid>
                </Grid>
            </Grid>
        </>
    )
}