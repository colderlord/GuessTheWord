import React, {Component} from 'react';
import {
    AppBar,
    Badge,
    Box,
    Drawer,
    IconButton,
    List,
    ListItem,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    Menu,
    MenuItem,
    Toolbar,
    Typography
} from "@mui/material";
import InboxIcon from '@mui/icons-material/Inbox';
import MailIcon from '@mui/icons-material/Mail';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MenuIcon from '@mui/icons-material/Menu';
import {AccountCircle} from "@mui/icons-material";
import {GameInfoService} from "../services/gameInfo.service";
import {NavLink} from "react-router-dom";

interface NavMenuProps {
    gameInfoService: GameInfoService
}

interface NavMenuState {
    anchorEl?: HTMLElement | null
    mobileMoreAnchorEl?: HTMLElement | null
    drawerOpened: boolean
}

export class NavMenu extends Component<NavMenuProps, NavMenuState> {
    static displayName = NavMenu.name;
    menuId = "menuId";
    mobileMenuId = "mobileMenuId";

    constructor (props: NavMenuProps) {
        super(props);

        this.state = {
            anchorEl: undefined,
            mobileMoreAnchorEl: undefined,
            drawerOpened: false
        };
    }

    setAnchorEl(el: HTMLElement | null) {
        this.setState({
            anchorEl: el
        })
    }

    setMobileMoreAnchorEl(el: HTMLElement | null) {
        this.setState({
            mobileMoreAnchorEl: el
        })
    }

    handleMenuClose() {
        this.setAnchorEl(null);
        this.handleMobileMenuClose();
    }

    handleMobileMenuClose() {
        this.setMobileMoreAnchorEl(null);;
    }

    handleProfileMenuOpen(event: React.MouseEvent<HTMLElement>) {
        this.setAnchorEl(event.currentTarget);
    }

    handleMobileMenuOpen(event: React.MouseEvent<HTMLElement>) {
        this.setMobileMoreAnchorEl(event.currentTarget);
    }

    renderMobileMenu () {
        return (
            <Menu
                anchorEl={this.state.mobileMoreAnchorEl}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                id={this.mobileMenuId}
                keepMounted
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                open={Boolean(this.state.mobileMoreAnchorEl)}
                onClose={() => this.handleMobileMenuClose()}
            >
                <MenuItem>
                    <IconButton size="large" aria-label="show 4 new mails" color="inherit">
                        <Badge badgeContent={4} color="error">
                            <MailIcon/>
                        </Badge>
                    </IconButton>
                    <p>Messages</p>
                </MenuItem>
                <MenuItem>
                    <IconButton
                        size="large"
                        aria-label="show 17 new notifications"
                        color="inherit"
                    >
                        <Badge badgeContent={17} color="error">
                            <NotificationsIcon/>
                        </Badge>
                    </IconButton>
                    <p>Notifications</p>
                </MenuItem>
                <MenuItem onClick={e => this.handleProfileMenuOpen(e)}>
                    <IconButton
                        size="large"
                        aria-label="account of current user"
                        aria-controls="primary-search-account-menu"
                        aria-haspopup="true"
                        color="inherit"
                    >
                        <AccountCircle/>
                    </IconButton>
                    <p>Profile</p>
                </MenuItem>
            </Menu>
        );
    }

    renderMenu () {
        return (
            <Menu
                anchorEl={this.state.anchorEl}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                id={this.menuId}
                keepMounted
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                open={Boolean(this.state.anchorEl)}
                onClose={() => this.handleMenuClose()}
            >
                <MenuItem onClick={() => this.handleMenuClose()}>Profile</MenuItem>
                <MenuItem onClick={() => this.handleMenuClose()}>My account</MenuItem>
            </Menu>
        );
    }

    toggleDrawer() {
        this.setState(
            {
                drawerOpened: !this.state.drawerOpened
            }
        )
    }

    onClickMenuItem() {
        
        this.toggleDrawer();
    }

    renderMenuItems() {
        return (
            // @ts-ignore
            <Box
                sx={{ width: 250 }}
                role="presentation"
                anchor="left"
            >
                <List>
                    {this.props.gameInfoService.GetInfos().map(e => {
                        return <ListItem key={e.uid} disablePadding>
                            <NavLink
                                exact
                                to={e.route}
                                component={ListItemButton}
                                onClick={e => this.onClickMenuItem()}
                            >
                                <ListItemIcon>
                                    <InboxIcon />
                                </ListItemIcon>
                                <ListItemText primary={e.name} />
                            </NavLink>
                        </ListItem>
                    })}
                </List>
            </Box>
        );
    }

    render () {
        return (
            <Box sx={{ display: 'flex' }}>
                <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
                    <Toolbar>
                        <IconButton
                            size="large"
                            edge="start"
                            color="inherit"
                            aria-label="open drawer"
                            sx={{ mr: 2 }}
                            onClick={e => this.toggleDrawer()}
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography
                            variant="h6"
                            noWrap
                            component="a"
                            href="/"
                            sx={{
                                mr: 2,
                                display: { xs: 'none', md: 'flex' },
                                fontFamily: 'monospace',
                                fontWeight: 700,
                                letterSpacing: '.3rem',
                                color: 'inherit',
                                textDecoration: 'none',
                            }}
                        >
                            MUI
                        </Typography>
                        <Box sx={{ flexGrow: 1 }} />
                        <Box sx={{ display: { xs: 'none', md: 'flex' } }}>
                            <IconButton size="large" aria-label="show 4 new mails" color="inherit">
                                <Badge badgeContent={4} color="error">
                                    <MailIcon />
                                </Badge>
                            </IconButton>
                            <IconButton
                                size="large"
                                aria-label="show 17 new notifications"
                                color="inherit"
                            >
                                <Badge badgeContent={17} color="error">
                                    <NotificationsIcon />
                                </Badge>
                            </IconButton>
                            <IconButton
                                size="large"
                                edge="end"
                                aria-label="account of current user"
                                aria-controls={this.menuId}
                                aria-haspopup="true"
                                onClick={e => this.handleProfileMenuOpen(e)}
                                color="inherit"
                            >
                                <AccountCircle />
                            </IconButton>
                        </Box>
                    </Toolbar>
                </AppBar>
                {this.renderMenu()}
                {this.renderMobileMenu()}
                <Drawer
                    variant="persistent"
                    sx={{
                        width: 250,
                        flexShrink: 0,
                        [`& .MuiDrawer-paper`]: { width: 250, boxSizing: 'border-box' },
                    }}
                    open={this.state.drawerOpened}
                    onClose={e => this.toggleDrawer()}
                >
                    <Toolbar />
                    {this.renderMenuItems()}
                </Drawer>
            </Box>
        );
    }
}