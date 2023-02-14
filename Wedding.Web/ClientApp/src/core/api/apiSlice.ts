import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/dist/query/react'
import {BASE_API_URL} from "core/constants/apiRouteConstants";


export const apiSlice = createApi({
    reducerPath: 'apiSlice',
    tagTypes: [],
    baseQuery: fetchBaseQuery({
        baseUrl: BASE_API_URL,
        prepareHeaders: (headers, {getState}) => {
            return headers
        },
    }),

    endpoints: () => ({}),
})