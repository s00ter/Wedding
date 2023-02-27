import {apiSlice} from "core/api/apiSlice";
import {ORDER_API_PATH} from "core/constants/apiRouteConstants";

type CreateOrderBody = {
    phone: string
    paymentMethod: string
    orderItems: OrderItem[]
}

type OrderItem = {
    wareId: string
    quantity: number
}

const ordersApi = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        createOrder: builder.mutation<null, CreateOrderBody>({
            query: (body) => ({
                method: 'POST',
                url: ORDER_API_PATH,
                body
            }),
        }),
    }),
    overrideExisting: false,
})

// hooks
export const {useCreateOrderMutation} = ordersApi