import {
    AppBar,
    CssBaseline,
    Grid,
    Slide,
    Toolbar,
    Typography,
    useScrollTrigger
} from "@mui/material";
import React from "react";
import {Outlet} from "react-router-dom";

function HideOnScroll(props: any) {
    const {children, window} = props;
    const trigger = useScrollTrigger({
        target: window ? window() : undefined,
    });

    return (
        <Slide appear={false} direction="down" in={!trigger}>
            {children}
        </Slide>
    );
}

export const Navbar : React.FC = (props) => {
    return (
        <>
            <CssBaseline/>
            <HideOnScroll {...props}>
                <AppBar style={{background: '#2E3B55'}}>
                    <Toolbar>
                        <Typography variant="body2" color="inherit" sx={{flexGrow: 1}}>
                            Свадебный салон "Валентина"
                        </Typography>
                        <Grid alignContent={'flex-end'}>
                            <Typography variant="body2" mr={'50px'} color="inherit">
                                О нас
                            </Typography>
                            <Typography variant="body2" mr={'50px'} color="inherit">
                                Отзывы
                            </Typography>
                            <Typography variant="body2" color="inherit">
                                Салоны
                            </Typography>
                        </Grid>
                    </Toolbar>
                </AppBar>
            </HideOnScroll>
            <Toolbar/>
            <Outlet/>
        </>
    );
};