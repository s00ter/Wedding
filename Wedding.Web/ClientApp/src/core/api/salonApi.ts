import {SALON_API_PATH} from '../constants/apiRouteConstants'
import {apiSlice} from "../api/apiSlice";

type SalonType = {
    id: number
    address: string
    latitude: number
    longitude: number
    city: CityType
}

type CityType = {
    name: string
}

const salonsApi = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getSalons: builder.query<SalonType[], null>({
            query: () => SALON_API_PATH
        }),
    }),
    overrideExisting: false,
})

// hooks
export const {useGetSalonsQuery} = salonsApi