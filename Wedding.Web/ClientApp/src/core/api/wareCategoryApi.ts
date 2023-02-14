import {CATEGORIES_API_PATH} from '../constants/apiRouteConstants'
import {apiSlice} from "../api/apiSlice";

type getCategoryItemsBodyType = {
    skip: number
    take: number
    categoryId: string
    priceFrom: number | null
    priceTo: number | null
    search: string | null
}

type categoryType = {
    id: string
    name: string
}

const categoriesApi = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getCategories: builder.query<categoryType[], null>({
            query: () => CATEGORIES_API_PATH
        }),
        getCategoryItems: builder.query<any, getCategoryItemsBodyType>({
            query: (body) => ({
                url: CATEGORIES_API_PATH,
                method: 'POST',
                body
            })
        }),
    }),
    overrideExisting: false,
})

// hooks
export const {useGetCategoriesQuery, useGetCategoryItemsQuery} = categoriesApi