﻿import {createSlice} from '@reduxjs/toolkit';
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
        removeItem: (state, action) => {
            const item = state.cart.find((item) => item.id === action.payload.id)
            if (item?.quantity === 1) {
                state.cart = state.cart.filter((item) => item.id !== action.payload.id)
            }

            item && item.quantity--
        },
        clearCart: (state) => {
            console.log(state.cart)
            state.cart = []
            console.log(state.cart)
        }
    },
})

export const {
    addToCart,
    removeItem,
    clearCart
} = cartSlice.actions;

export const selectCartItems = (state: RootState) => state.cart.cart