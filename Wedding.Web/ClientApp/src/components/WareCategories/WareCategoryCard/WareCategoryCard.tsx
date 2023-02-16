import {Card, CardContent, Grid, Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";
import {retrieveCategoryImageUrl} from "core/helpers/imageHelper";

export const WareCategoryCard = (props: {id: string; name: string }) => {

    const navigate = useNavigate()

    const onCardClick = () => {
        navigate(`category/${props.id}`)
    }

    return (
        <Grid item xs={4}>
            <Card sx={{ maxWidth: 500, cursor: 'pointer' }} onClick={onCardClick}>
                <img src={retrieveCategoryImageUrl(props.id)} alt='no image' style={{height: '150px'}}/>
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        {props.name}
                    </Typography>
                </CardContent>
            </Card>
        </Grid>
    );
}