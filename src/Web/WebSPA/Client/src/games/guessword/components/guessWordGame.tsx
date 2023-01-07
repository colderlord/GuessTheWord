import React, {Component} from "react";
import {Box, Toolbar} from "@mui/material";
import { RouteComponentProps } from "react-router-dom";
import { guessWordService } from "../../../App";

interface RouteParams {
    id: string
}

export interface GuessWordGameState {

}

export class GuessWordGame extends Component<RouteComponentProps<RouteParams>, GuessWordGameState> {

    componentDidMount() {
        const id = this.props.match.params.id;
        guessWordService.load(id);
    }

    render() {
        return <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
            <Toolbar />
            <h1>Игра GuessGame id = {this.props.match.params.id}!</h1>
        </Box>;
    }
}