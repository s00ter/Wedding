import {Box, Card, CardContent, CardMedia, Grid, Tooltip, Typography} from "@mui/material";
import AlarmIcon from "@mui/icons-material/Alarm";
import {useDispatch, useSelector} from "react-redux";
import {addToCart, CartItem, removeItem, selectCartItems} from "core/store/cartSlice";
import {retrieveWareImageUrl} from "core/helpers/imageHelper";

export const WareCategoryItem = (props: { id: string; name: string; description: string; price: number; discounted: boolean }) => {
    const dispatch = useDispatch()

    const cartItems = useSelector(selectCartItems)

    const onBuyBtnClick = () => {
        dispatch(addToCart({id: props.id, quantity: 1, price: props.price}))
    }

    const onCancelBuyBtnClick = () => {
        dispatch(removeItem(props.id))
    }

    return (<Card id={props.id} sx={{display: 'flex', marginBottom: '5px'}}>
        <CardMedia
            component="img"
            sx={{width: 300}}
            image={retrieveWareImageUrl(props.id)}
            alt="Image"
        />
        <Box sx={{display: 'flex', flexDirection: 'column'}}>
            <CardContent sx={{flex: '1 0 auto'}}>
                <Typography component="div" variant="h5">
                    {props.name}
                </Typography>
                <Typography variant="subtitle1" color="text.secondary" component="div">
                    {props.description}
                </Typography>
                <Typography variant="subtitle1" color="text.secondary" component="div">
                    {props.price} p.
                </Typography>
                {props.discounted &&
                    <Tooltip title="Акция. Время действия ограничено.">
                        <AlarmIcon sx={{cursor: 'pointer'}}/>
                    </Tooltip>}
                {cartItems.some((i: CartItem) => i.id === props.id)
                    ? (
                    <Grid onClick={onCancelBuyBtnClick} sx={{cursor: 'pointer'}}>
                        Удалить из корзины
                    </Grid>
                    )
                    : (
                    <Grid onClick={onBuyBtnClick} sx={{cursor: 'pointer'}}>
                        Добавить в корзину
                    </Grid>
                    )}
            </CardContent>
        </Box>
    </Card>)
}