import React, {Component} from "react";
import {RouteComponentProps} from "react-router-dom";
import {observer} from "mobx-react";
import {Start} from "@mui/icons-material";
import {Box, Button, Toolbar} from "@mui/material";
import {guessWordService} from "../../../App";
import {GuessGame} from "../models/guessGame";
import {GuessGameSettings} from "../models/guessGameSettings";
import GuessWordGameField from "./guessWordGameField";

interface RouteParams {
    id: string
}

export interface GuessWordGameState {
    game: GuessGame,
    loading: boolean
}

class GuessWordGame extends Component<RouteComponentProps<RouteParams>, GuessWordGameState> {
    constructor(props: RouteComponentProps<RouteParams>) {
        super(props);
        this.state = {
            game: new GuessGame(new GuessGameSettings()),
            loading: true
        }
    }

    async componentDidMount() {
        const id = this.props.match.params.id;
        const game = await guessWordService.load(id);
        this.setState({
            game: game,
            loading: false
        })
    }

    async onStart() {
        this.setState({
            loading: true
        })

        const startDate = await guessWordService.startGame(this.state.game.id);
        this.state.game.setStartDate(startDate);

        this.setState({
            loading: false
        });
    }

    render() {
        return <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
            <Toolbar />
            <h1>Игра GuessGame от = {this.state.game.creationDate.toString()}!</h1>
            {!this.state.game.startDate
                ?
                <Button
                    startIcon={<Start/>}
                    onClick={_ => this.onStart()}
                >
                    Начать игру
                </Button>
                : <></>
            }
            <GuessWordGameField game={this.state.game} />
        </Box>;
    }
}

export default observer(GuessWordGame)