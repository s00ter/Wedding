import {apiSlice} from "core/api/apiSlice";
import {SALON_API_PATH} from "core/constants/apiRouteConstants";

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