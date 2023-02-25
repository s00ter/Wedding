﻿import {Route, Routes} from "react-router-dom";
import {Navbar} from "components/Navbar/Navbar";
import {WareCategories} from "components/WareCategories/WareCategories";
import {WareCategoryItems} from "components/WareCategoryItems/WareCategoryItems";
import {Salons} from "components/Salons/Salons";


export const App = () => {
    return (
        <>
            <Routes>
                <Route path='/' element={<Navbar/>}>
                    <Route path='/' element={<WareCategories/>}/>
                    <Route path='/salons' element={<Salons/>}/>
                    <Route path='category/:id' element={<WareCategoryItems/>}/>
                </Route>
            </Routes>
        </>
    );
}

export default App