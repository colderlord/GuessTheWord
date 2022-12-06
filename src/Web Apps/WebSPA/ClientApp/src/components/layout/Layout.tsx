import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import {createTheme, ThemeProvider, useTheme} from '@mui/material/styles';
import Brightness4Icon from '@mui/icons-material/Brightness4';
import Brightness7Icon from '@mui/icons-material/Brightness7';
import CssBaseline from "@mui/material/CssBaseline";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Link from "@mui/material/Link";
import GlobalStyles from '@mui/material/GlobalStyles';
import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";

import {LangInfo, Storage} from "../../storage/Storage";
import {observer} from "mobx-react";

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
    storage: Storage
}

function LanguageSelector(props: LanguageSelectorProps) {
    function onChangeLanguage(langInfo: LangInfo) : void {
        props.storage.setLang(langInfo)
    }

    return (
        <Autocomplete
            id="lang-selector"
            size={"small"}
            value={props.storage.currentLangInfo}
            onChange={(event: any, newValue: LangInfo) => {
                onChangeLanguage(newValue);
            }}
            defaultValue={props.storage.currentLangInfo}
            disableClearable={true}
            loading={props.storage.langInfosStorage.loadingInfos}
            options={props.storage.langInfosStorage.languageInfos}
            getOptionLabel={(option) => option.name}
            onOpen={async () => {
                await props.storage.langInfosStorage.getLanguagesInfosAsync();
            }}
            sx={{ width: 150 }}
            renderInput={(params) => <TextField {...params} />}
        />
    )
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
    children?: React.ReactNode;

    storage: Storage;
}

function Layout(props: LayoutProps) {
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
                    <LanguageSelector storage={props.storage}/>
                </Toolbar>
                {props.children}
                <Copyright />
            </ThemeProvider>
        </ColorModeContext.Provider>
    );
}

export default observer(Layout)