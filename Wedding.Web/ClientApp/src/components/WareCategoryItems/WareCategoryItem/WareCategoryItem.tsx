import {Box, Card, CardContent, CardMedia, Tooltip, Typography} from "@mui/material";
import AlarmIcon from "@mui/icons-material/Alarm";

export const WareCategoryItem = (props: { id: string; name: string; description: string; price: number; discounted: boolean }) => {

    const photo = 'https://www.hdwallpapers.in/download/darling_in_the_franxx_blue_eyes_zero_two_with_background_of_volleyball_net_hd_anime-HD.jpg'

    return (<Card id={props.id} sx={{display: 'flex', marginBottom: '5px'}}>
        <CardMedia
            component="img"
            sx={{width: 300}}
            image={photo}
            alt="Live from space album cover"
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
            </CardContent>
        </Box>
    </Card>)
}