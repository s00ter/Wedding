import {Route, Routes} from "react-router-dom";
import {Navbar} from "components/Navbar/Navbar";
import {WareCategories} from "components/WareCategories/WareCategories";
import {WareCategoryItems} from "components/WareCategoryItems/WareCategoryItems";


export const App = () => {
    return (
        <>
            <Routes>
                <Route path='/' element={<Navbar/>}>
                    <Route path='/' element={<WareCategories/>}/>
                    <Route path='category/:id' element={<WareCategoryItems/>}/>
                </Route>
            </Routes>
        </>
    );
}

export default App