import {useDispatch, useSelector} from "react-redux";
import {addToCart, removeItem, selectCartItems} from "core/store/cartSlice";
import {useGetWaresByIdsQuery} from "core/api/wareApi";
import {FormControlLabel, Grid, LinearProgress, OutlinedInput, Radio, RadioGroup, Typography} from "@mui/material";
import {retrieveWareImageUrl} from "core/helpers/imageHelper";
import {useState} from "react";

export const Cart = () => {
    const [paymentMethod, setPaymentMethod] = useState("Наличные")
    const [phoneNumber, setPhoneNumber] = useState<string | undefined>(undefined)

    const cartItems = useSelector(selectCartItems)
    const dispatch = useDispatch()

    const queryBody = {wareIds: cartItems.map(i => i.id)}

    const {isFetching: isWaresFetching, data: wares} = useGetWaresByIdsQuery(queryBody)

    if (cartItems.length === 0) {
        return <Typography textAlign={'center'} mt={'50px'} fontSize={'40px'}>Нет товаров в корзине</Typography>
    }

    const onWareCountChange = (id: string, isReduce: boolean) => {
        if (isReduce) {
            dispatch(removeItem({id: id}))
            return
        }

        dispatch(addToCart({id: id}))
    }

    const handlePaymentMethodChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPaymentMethod(event.target.value);
    }

    const onOrderConfirmClick = () => {
        
    }

    if (isWaresFetching) {
        return <LinearProgress/>
    }

    return (
        <Grid ml={'50px'} mt={'50px'} direction={'row'} container>
            <Grid container direction='column' width='500px'>
                {wares && wares.map(w => (
                    <Grid direction='row' container bgcolor='#FFFFFF' mb={'10px'} borderRadius={'15px'}>
                        <Grid item>
                            <img src={retrieveWareImageUrl(w.id)} alt={'img'} width='100px' height='70px'/>
                        </Grid>
                        <Grid item width={'300px'}>
                            <Grid item>
                                <Typography>{w.name}</Typography>
                                <Typography mt='15px'>Стоимость: {w.retailPrice} p.</Typography>
                            </Grid>
                        </Grid>
                        <Grid item>
                            <Grid container direction={'row'} mt={'15px'}>
                                <Grid bgcolor={'#E6E6E3'} mr={'5px'} width={'20px'} height={'23px'} textAlign={'center'} borderRadius={'25px'}
                                    sx={{cursor: 'pointer'}} onClick={() => onWareCountChange(w.id, false)}>+</Grid>
                                <Typography>{cartItems.find(c => c.id === w.id)?.quantity}</Typography>
                                <Grid bgcolor={'#E6E6E3'} ml={'5px'} width={'20px'} height={'23px'} textAlign={'center'} borderRadius={'25px'}
                                      sx={{cursor: 'pointer'}} onClick={() => onWareCountChange(w.id, true)}>-</Grid>
                            </Grid>
                        </Grid>
                    </Grid>
                ))}
                <Grid item bgcolor='#FFFFFF' borderRadius={'15px'} pt={'5px'} pb={'5px'}>
                    <Typography>Всего: {cartItems.map(c => c.quantity * c.price).reduce((partialSum, a) => partialSum + a, 0)} p.</Typography>
                </Grid>
            </Grid>
            <Grid ml={'20px'} bgcolor='#FFFFFF' borderRadius={'15px'} p='15px'>
                <Typography>Метод оплаты:</Typography>
                <RadioGroup
                    aria-labelledby="demo-controlled-radio-buttons-group"
                    name="controlled-radio-buttons-group"
                    value={paymentMethod}
                    onChange={handlePaymentMethodChange}
                >
                    <FormControlLabel value="Наличные" control={<Radio />} label="Наличные" />
                    <FormControlLabel value="Безналичная оплата" control={<Radio />} label="Безналичная оплата" />
                    <FormControlLabel value="Кредит/рассрочка" control={<Radio />} label="Кредит/рассрочка" />
                </RadioGroup>
                <OutlinedInput placeholder={'Номер телефона'} fullWidth value={phoneNumber}
                               onChange={e => setPhoneNumber(e.target.value)}/>
                <Grid border={'1px solid black'} sx={{cursor: 'pointer'}} borderRadius={'25px'} mt={'5px'} textAlign={'center'}>
                    Оформить заказ
                </Grid>
            </Grid>
        </Grid>
    )
}