import {Card, CardContent, CardMedia, Grid, Typography} from "@mui/material";

export const WareCategoryCard = (props: { photo: string; name: string }) => {
    return (
        <Grid item xs={4}>
            <Card sx={{ maxWidth: 500, cursor: 'pointer' }}>
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