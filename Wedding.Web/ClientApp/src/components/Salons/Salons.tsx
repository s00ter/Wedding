import {useGetSalonsQuery} from "core/api/salonApi";
import {Grid, LinearProgress, Typography} from "@mui/material";
import {retrieveSalonImageUrl} from "core/helpers/imageHelper";

export const Salons = () => {

    const {data: salons, isFetching: isSalonsFetching } = useGetSalonsQuery(null)

    if (isSalonsFetching) {
        return <LinearProgress/>
    }

    return (
        <Grid container direction='column' marginTop={'50px'}>
            {salons && salons.map(s => (
                <Grid item container bgcolor={'#FFFFFF'} m={'15px'} ml={'150px'} width={'600px'}>
                    <Grid item>
                        <img src={retrieveSalonImageUrl(s.id)} alt='no image' style={{height: '150px', width: '200px'}}/>
                    </Grid>
                    <Grid item mt={'25px'} ml={'15px'}>
                        <Typography variant="h5">{s.city.name}, {s.address}</Typography>
                        <Typography>{'+375 (29) 652 57 34'}</Typography>
                    </Grid>
                </Grid>
            ))}
        </Grid>
    )
}