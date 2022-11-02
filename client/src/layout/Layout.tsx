import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import {createTheme, ThemeProvider, useTheme} from '@mui/material/styles';
import Brightness4Icon from '@mui/icons-material/Brightness4';
import Brightness7Icon from '@mui/icons-material/Brightness7';
import CssBaseline from "@mui/material/CssBaseline";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import Link from "@mui/material/Link";
import GlobalStyles from '@mui/material/GlobalStyles';

import { Storage } from "../storage/Storage";
import { Settings } from "../interfaces/Settings";

const ColorModeContext = React.createContext({ toggleColorMode: () => {} });

function ThemeSelector() {
    const theme = useTheme();
    const colorMode = React.useContext(ColorModeContext);
    return (
        <IconButton sx={{ ml: 1 }} onClick={colorMode.toggleColorMode} color="inherit">
            {theme.palette.mode === 'dark' ? <Brightness7Icon /> : <Brightness4Icon />}
        </IconButton>
    );
}

interface LanguageSelectorProps {
    settings: Settings
}

function LanguageSelector(props: LanguageSelectorProps) {
    function onChangeLanguage(e: any) : void {
        props.settings.SetCulture(e.target.value);
    }

    return (<FormControl sx={{ my: 1, mx: 1.5 }}>
        <InputLabel id="language-select-label">Язык</InputLabel>
        <Select
            labelId="demo-language-select-label"
            id="language-simple-select"
            value={props.settings.Culture}
            label="Язык"
            size={"small"}
            onChange={onChangeLanguage}
        >
            <MenuItem value={"ru-RU"}>Русский</MenuItem>
            <MenuItem value={"en-US"}>English</MenuItem>
        </Select>
    </FormControl>)
}

function Copyright() {
    return (
        <Typography variant="body2" color="text.secondary" align="center">
            {'Copyright © '}
            <Link color="inherit" href="https://mui.com/">
                colderlord
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

export interface LayoutProps {
    /**
     * Children of layout
     *
     * @type {React.ReactNode}
     * @memberof LayoutProps
     */
    children?: React.ReactNode;

    storage: Storage;
}

export default function Render(props: LayoutProps) {
    const [mode, setMode] = React.useState<'light' | 'dark'>('light');
    const colorMode = React.useMemo(
        () => ({
            toggleColorMode: () => {
                setMode((prevMode) => (prevMode === 'light' ? 'dark' : 'light'));
            },
        }),
        [],
    );

    const theme = React.useMemo(
        () =>
            createTheme({
                palette: {
                    mode,
                },
            }),
        [mode],
    );

    return (
        <ColorModeContext.Provider value={colorMode}>
            <ThemeProvider theme={theme}>
                <GlobalStyles styles={{ ul: { margin: 0, padding: 0, listStyle: 'none' } }} />
                <CssBaseline />
                <Toolbar sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <Typography
                        component="h2"
                        variant="h5"
                        color="inherit"
                        align="center"
                        noWrap
                        sx={{ flex: 1 }}
                    />
                    <ThemeSelector />
                    <LanguageSelector settings={props.storage.Settings}/>
                </Toolbar>
                {props.children}
                <Copyright />
            </ThemeProvider>
        </ColorModeContext.Provider>
    );
}
