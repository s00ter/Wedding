import {createSlice} from '@reduxjs/toolkit';
import {RootState} from "core/store/store";

export type CartItem = {
    id: string
    quantity: number
    price: number
}

export const cartSlice = createSlice({
    name: 'cart',
    initialState: {
        cart: [] as CartItem[],
    },
    reducers: {
        addToCart: (state, action) => {
            const itemInCart = state.cart.find((item) => item.id === action.payload.id)
            if (itemInCart) {
                itemInCart.quantity++
            } else {
                state.cart.push({ ...action.payload, quantity: 1 })
            }
        },
        incrementQuantity: (state, action) => {
            const item = state.cart.find((item) => item.id === action.payload)
            if (item) {
                item.quantity++
            }
        },
        decrementQuantity: (state, action) => {
            const item = state.cart.find((item) => item.id === action.payload);
            if (item?.quantity === 1) {
                item.quantity = 1
                return
            }

            item && item.quantity--
        },
        removeItem: (state, action) => {
            const item = state.cart.find((item) => item.id === action.payload);
            if (item?.quantity === 1) {
                state.cart = state.cart.filter((item) => item.id !== action.payload);
            }

            item && item.quantity--
        },
    },
})

export const cartReducer = cartSlice.reducer;
export const {
    addToCart,
    incrementQuantity,
    decrementQuantity,
    removeItem,
} = cartSlice.actions;

export const selectCartItems = (state: RootState) => state.cart.cart