﻿import * as React from 'react';
import Grid from "@mui/material/Grid";
import {observer} from "mobx-react";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import InputBase from "@mui/material/InputBase";
import Divider from "@mui/material/Divider";
import IconButton from "@mui/material/IconButton";
import AddIcon from "@mui/icons-material/Add";
import GuessGameContent from "./GuessGameContent";
import GuessGameModel from "../../../storage/Games/GuessGameModel";
import {Storage} from "../../../storage/Storage";
import FormHelperText from "@mui/material/FormHelperText";

export interface GuessGameProps {
    storage: Storage;
}

function GuessGame(props: GuessGameProps) {
    const [loading, setLoading] = React.useState<boolean>(false);
    const [error, setError] = React.useState<string>("");
    const [value, setValue] = React.useState<string>("");

    async function onAdd() {
        const settings = props.storage.settings;
        const gameModel = props.storage.gameModel as GuessGameModel;
        if (gameModel) {
            if (settings.attempts == gameModel.wordModel.length) {
                return;
            }
            setLoading(true);
            try {
                //await gameModel.AddWord(value);
                setLoading(false);
            }
            catch (e) {
                setLoading(false);
            }
        }
        setValue("");
        setError("");
    }

    function onChange(v: string) {
        setValue(v);
    }

    function errorText() {
        if (error.length > 0) {
            return <FormHelperText error>{error}</FormHelperText>;
        }
        return <></>;
    }

    return (<React.Fragment>
        <Typography variant="h6" gutterBottom>
            Игра началась!
        </Typography>
        <Grid container>
            <Grid item xs={12}>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <Paper
                            component="form"
                            sx={{p: '2px 4px', display: 'flex', alignItems: 'center'}}
                        >
                            <InputBase
                                sx={{ml: 1, flex: 1}}
                                placeholder="Введите слово"
                                inputProps={{'aria-label': 'Введите слово'}}
                                value={value}
                                onChange={e => onChange(e.currentTarget.value)}
                                error={error.length > 0}
                                required
                            />
                            <Divider sx={{height: 28, m: 0.5}} orientation="vertical"/>
                            <IconButton color="primary" sx={{p: '10px'}} aria-label="directions" onClick={onAdd}>
                                <AddIcon/>
                            </IconButton>
                        </Paper>
                        {errorText()}
                    </Grid>
                </Grid>
            </Grid>
            <GuessGameContent game={props.storage.gameModel as GuessGameModel} loading={loading} />
        </Grid>
    </React.Fragment>)
}
export default observer(GuessGame)