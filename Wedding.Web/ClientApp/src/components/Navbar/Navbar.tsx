import {
    AppBar, Badge, BadgeProps,
    CssBaseline,
    Grid,
    Slide, styled,
    Toolbar,
    Typography,
    useScrollTrigger
} from "@mui/material";
import React from "react";
import {Outlet, useNavigate} from "react-router-dom";
import {useSelector} from "react-redux";
import {selectCartItems} from "core/store/cartSlice";
import {CartIcon} from "Icons/CartIcon/CartIcon";

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

const StyledBadge = styled(Badge)<BadgeProps>(({ theme }) => ({
    '& .MuiBadge-badge': {
        right: 50,
        top: 40,
        padding: '0 4px',
    },
}));

export const Navbar: React.FC = (props) => {
    const navigate = useNavigate();

    const cartItems = useSelector(selectCartItems)

    let sum = 0;
    for (let i = 0; i < cartItems.length; i++) {
        sum += cartItems[i].price
    }

    return (
        <>
            <CssBaseline/>
            <HideOnScroll {...props}>
                <AppBar style={{background: '#2E3B55'}}>
                    <Toolbar sx={{justifyContent: 'space-between'}}>
                        <Grid container sx={{cursor: 'pointer'}} onClick={() => navigate('/')}>
                            <Typography variant="body2" color="inherit" sx={{flexGrow: 1}}>
                                Свадебный салон "Валентина"
                            </Typography>
                        </Grid>
                        <Grid container>
                            <Grid item>
                                <Typography variant="body2" mr={'50px'} color="inherit">
                                    О нас
                                </Typography>
                            </Grid>
                            <Grid item>
                                <Typography variant="body2" mr={'50px'} color="inherit">
                                    Отзывы
                                </Typography>
                            </Grid>
                            <Grid item sx={{cursor: 'pointer'}} onClick={() => navigate('/salons')}>
                                <Typography variant="body2" color="inherit">
                                    Салоны
                                </Typography>
                            </Grid>
                        </Grid>
                        {cartItems && cartItems.length !== 0 && (
                            <Grid sx={{cursor: 'pointer'}} onClick={() => navigate('/cart')}>
                                <StyledBadge badgeContent={cartItems.length} color="secondary">
                                    <CartIcon width='50px' height='50px'/>
                                </StyledBadge>
                            </Grid>
                        )}
                    </Toolbar>
                </AppBar>
            </HideOnScroll>
            <Toolbar/>
            <Outlet/>
        </>
    );
};