import {CATEGORIES_API_PATH, PRICE_RANGES_API_PATH} from '../constants/apiRouteConstants'
import {apiSlice} from "../api/apiSlice";

type GetCategoryItemsBodyType = {
    skip: number
    take: number
    categoryId: string
    priceFrom: number | null
    priceTo: number | null
    search: string | null
    priceDesc: boolean
}

type CategoryType = {
    id: string
    name: string
    photo: string | null
}

type CategoryItemType = {
    id: string
    name: string
    description: string
    price: number
    discounted: boolean
}

export type CategoryItemsResponseType = {
    items: CategoryItemType[]
    total: number
}

type CategoryRangesType = {
    min: number
    max: number
}

const categoriesApi = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getCategories: builder.query<CategoryType[], null>({
            query: () => CATEGORIES_API_PATH
        }),
        getCategoryRanges: builder.query<CategoryRangesType, string>({
            query: (categoryId) => PRICE_RANGES_API_PATH + categoryId
        }),
        getCategoryItems: builder.query<CategoryItemsResponseType, GetCategoryItemsBodyType>({
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
export const {useGetCategoriesQuery, useGetCategoryRangesQuery, useLazyGetCategoryItemsQuery} = categoriesApi