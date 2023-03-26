import React, {Component} from "react";
import {RouteComponentProps} from "react-router-dom";
import {observer} from "mobx-react";
import {Start} from "@mui/icons-material";
import {Box, Button, Skeleton, Toolbar} from "@mui/material";
import {guessWordService} from "../../../App";
import {GuessGame} from "../models/guessGame";
import GuessWordGameField from "./guessWordGameField";

interface RouteParams {
    id: string
}

export interface GuessWordGameState {
    game?: GuessGame,
    loading: boolean
}

class GuessWordGame extends Component<RouteComponentProps<RouteParams>, GuessWordGameState> {
    constructor(props: RouteComponentProps<RouteParams>) {
        super(props);
        this.state = {
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

        const startDate = await guessWordService.startGame(this.state.game!);

        this.setState({
            loading: false
        });
    }

    renderGame(game: GuessGame) {
        return <>
            <h1>Игра GuessGame от = {game.creationDate.toString()}!</h1>
            {!game.startDate
                ?
                <Button
                    startIcon={<Start/>}
                    onClick={_ => this.onStart()}
                >
                    Начать игру
                </Button>
                : <></>
            }
            <GuessWordGameField game={game} />
        </>
    }

    render() {
        const {game, loading} = this.state;
        if (loading) {
            return <></>
        }
        if (!game) {
            if (loading)
            return <></>
        }

        return <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
            <Toolbar />
            {loading
                ? <>
                    <Skeleton/>
                    <Skeleton/>
                    <Skeleton/>
                </>
                : <></>
            }
            {game ? this.renderGame(game) : <></>}
        </Box>;
    }
}

export default observer(GuessWordGame)