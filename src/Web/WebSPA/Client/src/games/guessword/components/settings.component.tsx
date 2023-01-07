import React, {Component} from "react";
import {Box, Toolbar} from "@mui/material";

export interface SettingsComponentProps {

}

export interface SettingsComponentState {

}

export class SettingsComponent extends Component<SettingsComponentProps, SettingsComponentState> {

    render() {

        return <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
            <Toolbar />
            <h1>Настройки!</h1>
        </Box>;
    }
}