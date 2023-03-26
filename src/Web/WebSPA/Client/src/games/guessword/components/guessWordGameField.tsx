import {observer} from "mobx-react";
import React, {Component} from "react";
import {Box, Paper} from "@mui/material";
import {GuessGame} from "../models/guessGame";
import WordItem from "./wordItem";
import WordAnswer from "./wordAnswer";
import {WordModel} from "../../../models/word.interface";

export interface GuessWordGameFieldProps {
    game: GuessGame
}

export interface GuessWordGameFieldState {
}

class GuessWordGameField extends Component<GuessWordGameFieldProps, GuessWordGameFieldState> {
    render() {
        const { game } = this.props;
        const settings = game.settings;
        let rows = [];
        for (let i = 0; i < settings.attempts; i++) {
            const historyItem = game.history[i];
            rows.push(
                <WordAnswer word={new WordModel()} editable={false} />
                // <WordItem wordLength={settings.wordLength} disabled={false} />
            )
        }

        return (
            <Box
                component="form"
                sx={{
                    '& .MuiTextField-root': { m: 1, width: '25ch' },
                }}
                noValidate
                autoComplete="off"
            >
                <Paper>
                    {rows}
                </Paper>
            </Box>);
    }
}

export default observer(GuessWordGameField)