import {BASE_API_URL, CATEGORIES_API_PATH} from "core/constants/apiRouteConstants";

export const retrieveCategoryImageUrl = (id: string) => {
    return BASE_API_URL + CATEGORIES_API_PATH + "/" + id + "/Image"
}