import {combineReducers, configureStore} from '@reduxjs/toolkit'
import {apiSlice} from "core/api/apiSlice";
import {cartSlice} from './cartSlice';
import storage from 'redux-persist/lib/storage';
import {
    persistReducer,
    FLUSH,
    REHYDRATE,
    PAUSE,
    PERSIST,
    PURGE,
    REGISTER,
} from 'redux-persist'

const reducers = combineReducers({ [apiSlice.reducerPath]: apiSlice.reducer, [cartSlice.name]: cartSlice.reducer});

const persistConfig = {
    key: 'root',
    storage
};
const persistedReducer = persistReducer(persistConfig, reducers);

export const store = configureStore({
    reducer: persistedReducer,
    middleware: (getDefaultMiddleware) => getDefaultMiddleware({
        serializableCheck: {
            ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
        },
    }).concat(apiSlice.middleware),
})

export type RootState = ReturnType<typeof store.getState>

