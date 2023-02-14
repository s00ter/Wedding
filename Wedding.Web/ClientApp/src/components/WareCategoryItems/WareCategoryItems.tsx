import {Card, Grid, Box, CardContent, CardMedia, Typography, LinearProgress} from "@mui/material";
import {useState} from "react";
import {useParams} from "react-router-dom";
import {useGetCategoryItemsQuery} from "core/api/wareCategoryApi";

const ELEMENTS_ON_PAGE = 10;

export const WareCategoryItems = () => {
    const [currentPage, setCurrentPage] = useState(0)
    const [activeItems, setActiveItems] = useState();

    const {id} = useParams()

    const {data: itemsResponse, isFetching: isFetchingItems} =
        useGetCategoryItemsQuery(
            {
                skip: currentPage * ELEMENTS_ON_PAGE,
                take: ELEMENTS_ON_PAGE,
                categoryId: id ?? '',
                search: null,
                priceFrom: null,
                priceTo: null
            })

    if (isFetchingItems) {
        return <LinearProgress />
    }

    return (
        <Grid>
            <Card sx={{display: 'flex'}}>
                {itemsResponse && itemsResponse.map}
                <CardMedia
                    component="img"
                    sx={{width: 300}}
                    image="/static/images/cards/live-from-space.jpg"
                    alt="Live from space album cover"
                />
                <Box sx={{display: 'flex', flexDirection: 'column'}}>
                    <CardContent sx={{flex: '1 0 auto'}}>
                        <Typography component="div" variant="h5">
                            Live From Space
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary" component="div">
                            Mac Miller
                        </Typography>
                    </CardContent>
                </Box>
            </Card>
        </Grid>
    )
}