import * as React from 'react';
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import InputBase from "@mui/material/InputBase";
import Divider from "@mui/material/Divider";
import AddIcon from '@mui/icons-material/Add';
import IconButton from '@mui/material/IconButton';
import FormHelperText from "@mui/material/FormHelperText";
import TextField from "@mui/material/TextField";
import {observer} from "mobx-react"
import {Storage} from "../storage/Storage";
import TryGuessGame from "./TryGuessGame";
import GuessGame from "./GuessGame";
import WordsGame from "./WordsGame";

export interface GameProps {
    storage: Storage;
}

function Game(props: GameProps) {
    const [value, setValue] = React.useState<string>("");
    const [error, setError] = React.useState<string>("");
    function onAdd() {
        const settings = props.storage.settings;
        if (value.length != settings.lettersCount) {
            setError("Длина слова должна быть равна " + settings.lettersCount);
            return;
        }

        const gameModel = props.storage.gameModel;
        if (gameModel) {
            if (settings.attempts == gameModel.wordModel.length) {
                return;
            }
            gameModel.AddWord(value);
        }
        setValue("");
        setError("");
    }
    
    function onChange(v: string) {
        const settings = props.storage.settings;
        if (v.length != settings.lettersCount) {
            setError("Длина слова должна быть равна " + settings.lettersCount);
            if (v.length > settings.lettersCount) {
                return;
            }
        } else {
            setError("");
        }
        setValue(v);
    }

    function errorText() {
        if (error.length > 0) {
            return <FormHelperText error>{error}</FormHelperText>;
        }
        return <></>;
    }

    function GetGame() {
        switch (props.storage.currentGameInfo.uid) {
            case "793370cf-7ea5-4d45-af27-65def7293e40": {
                return (<GuessGame storage={props.storage}/>);
            }
            case "b32bb668-5448-4b1b-89c9-31138bc6eb63": {
                return (<TryGuessGame game={props.storage.gameModel}/>);
            }
            case "b5451229-e47d-4fca-ad1b-c787c34d6279": {
                return (<WordsGame storage={props.storage}/>);
            }
            default: {
                return (<TextField label={"Игра не найдена"}/>);
            }
        }
    }

    return(<React.Fragment>
            <Typography variant="h6" gutterBottom>
                Игра началась!
            </Typography>
            <Grid container>
                <Grid item xs={12}>
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <Paper
                                component="form"
                                sx={{ p: '2px 4px', display: 'flex', alignItems: 'center' }}
                            >
                                <InputBase
                                    sx={{ ml: 1, flex: 1 }}
                                    placeholder="Введите слово"
                                    inputProps={{ 'aria-label': 'Введите слово' }}
                                    value={value}
                                    onChange={e => onChange(e.currentTarget.value)}
                                    error={error.length > 0}
                                    required
                                />
                                <Divider sx={{ height: 28, m: 0.5 }} orientation="vertical" />
                                <IconButton color="primary" sx={{ p: '10px' }} aria-label="directions" onClick={onAdd}>
                                    <AddIcon />
                                </IconButton>
                            </Paper>
                            {errorText()}
                        </Grid>
                    </Grid>
                </Grid>
                {GetGame()}
            </Grid>
    </React.Fragment>)
}

export default observer(Game)