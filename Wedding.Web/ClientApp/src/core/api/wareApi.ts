import {WARE_API_PATH} from '../constants/apiRouteConstants'
import {apiSlice} from "../api/apiSlice";

type GetWareItemsBodyType = {
    wareIds: string[]
}

type WareType = {
    id: string
    name: string
    description: string
    discounted: boolean
    category: string
    retailPrice: number
}

const wareApi = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getWaresByIds: builder.query<WareType[], GetWareItemsBodyType>({
            query: (body) => ({
                method: 'POST',
                url: WARE_API_PATH + '/Items',
                body
            }),
        }),
    }),
    overrideExisting: false,
})

// hooks
export const {useGetWaresByIdsQuery} = wareApi