import {Grid, LinearProgress} from "@mui/material";
import {WareCategoryCard} from "../WareCategories/WareCategoryCard/WareCategoryCard";
import {useGetCategoriesQuery} from "core/api/wareCategoryApi";
import React from "react";

export const WareCategories : React.FC = () => {

    const {data: categoriesResponse, isFetching: isFetchingCategories} =
        useGetCategoriesQuery(null)

    const photo = 'https://www.hdwallpapers.in/download/darling_in_the_franxx_blue_eyes_zero_two_with_background_of_volleyball_net_hd_anime-HD.jpg'

    if (isFetchingCategories) {
        return <LinearProgress />
    }

    return (
        <Grid container mt={2} spacing={2} ml={'50px'} mr={'50px'}>
            {categoriesResponse && categoriesResponse.map((item) => (
                <WareCategoryCard id={item.id} name={item.name} photo={photo}/>
            ))}
        </Grid>
    )
}