import {observer} from "mobx-react";
import React, {Component} from "react";
import {Box, Paper, TextField} from "@mui/material";
import {GuessGame} from "../models/guessGame";

export interface GuessWordGameFieldProps {
    game: GuessGame
}

export interface GuessWordGameFieldState {
}

class GuessWordGameField extends Component<GuessWordGameFieldProps, GuessWordGameFieldState> {
    renderRow(): React.ReactElement {
        let letters = [];
        for (let i = 0; i < this.props.game.settings.WordLength; i++) {
            letters.push(
                <TextField
                    key={i.toString()}
                    size="small"
                    InputProps={{
                        style: { width: `40px` },
                    }}
                />
            )
        }
        return (
            <Paper
                component="form"
                sx={{ p: '2px 4px', display: 'flex', alignItems: 'center', width: 400 }}
            >
                {letters}
            </Paper>
        );
    }

    render() {
        let rows = [];
        for (let i = 0; i < this.props.game.settings.Attemts; i++) {
            rows.push(
                this.renderRow()
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
                {rows}
            </Box>);
    }
}

export default observer(GuessWordGameField)