import {Grid, LinearProgress} from "@mui/material";
import {WareCategoryCard} from "../WareCategories/WareCategoryCard/WareCategoryCard";
import {useGetCategoriesQuery} from "core/api/wareCategoryApi";
import React from "react";

export const WareCategories : React.FC = () => {

    const {data: categoriesResponse, isFetching: isFetchingCategories} =
        useGetCategoriesQuery(null)
    if (isFetchingCategories) {
        return <LinearProgress />
    }

    return (
        <Grid container mt={2} spacing={2} ml={'50px'} mr={'50px'}>
            {categoriesResponse && categoriesResponse.map((item) => (
                <WareCategoryCard id={item.id} name={item.name}/>
            ))}
        </Grid>
    )
}