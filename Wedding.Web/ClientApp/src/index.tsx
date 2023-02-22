import { BrowserRouter } from 'react-router-dom';
import ReactDOM from 'react-dom'
import './styles/index.css'
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import React from 'react';
import {Provider} from "react-redux";
import { store } from 'core/store/store'
// @ts-ignore
import { PersistGate } from 'redux-persist/integration/react';
import {persistStore} from "redux-persist";

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

const persistor = persistStore(store);

ReactDOM.render(
    <BrowserRouter basename={baseUrl ?? undefined}>
        <Provider store={store}>
            <PersistGate persistor={persistor}>
                <App />
            </PersistGate>
        </Provider>
    </BrowserRouter>,
    document.getElementById('root')
)


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals()