import {Card, CardContent, CardMedia, Grid, Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";

export const WareCategoryCard = (props: {id: string; photo: string; name: string }) => {

    const navigate = useNavigate()

    const onCardClick = () => {
        navigate(`category/${props.id}`)
    }

    return (
        <Grid item xs={4}>
            <Card sx={{ maxWidth: 500, cursor: 'pointer' }} onClick={onCardClick}>
                <CardMedia
                    sx={{ height: 150 }}
                    image={props.photo}
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {props.name}
                    </Typography>
                </CardContent>
            </Card>
        </Grid>

    );
}