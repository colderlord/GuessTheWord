import * as React from 'react';
import {observer} from "mobx-react"
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import InputBase from "@mui/material/InputBase";
import Divider from "@mui/material/Divider";
import AddIcon from '@mui/icons-material/Add';
import IconButton from '@mui/material/IconButton';
import FormHelperText from "@mui/material/FormHelperText";

import TryGuessGameContent from "./TryGuessGameContent";
import TryGuessGameModel from "../../../storage/Games/TryGuessGameModel";
import {WordModel} from "../../../storage/WordModel";

export interface TryGuessGameProps {
    gameModel: TryGuessGameModel;
}

function TryGuessGame(props: TryGuessGameProps) {
    const [value, setValue] = React.useState<string>("");
    const [error, setError] = React.useState<string>("");
    const [loading, setLoading] = React.useState<boolean>(false);
    const [disabled, setDisabled] = React.useState<boolean>(false);
    async function onAdd() {
        const settings = props.gameModel.settings;
        const gameModel = props.gameModel;
        if (gameModel) {
            if (settings.attempts == gameModel.wordModel.length) {
                return;
            }
            setLoading(true);
            try {
                await gameModel.AddWord(value);
                setLoading(false);
            }
            catch (e) {
                setLoading(false);
            }
        }
        setDisabled(true);
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

    async function onSelectWord(word: WordModel) {
        const gameModel = props.gameModel;
        if (gameModel) {
            setDisabled(true);
            setLoading(true);
            try {
                await gameModel.AddWordModel(word);
                setLoading(false);
            } catch (e) {
                setLoading(false);
            }
        }
    }

    return(<React.Fragment>
            <Grid container>
                {disabled === false
                    ?
                    <Grid item xs={12}>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                <Paper
                                    component="form"
                                    sx={{p: '2px 4px', display: 'flex', alignItems: 'center'}}
                                >
                                    <InputBase
                                        sx={{ml: 1, flex: 1}}
                                        placeholder="?????????????? ??????????"
                                        inputProps={{'aria-label': '?????????????? ??????????'}}
                                        value={value}
                                        onChange={e => onChange(e.currentTarget.value)}
                                        error={error.length > 0}
                                        required
                                    />
                                    <Divider sx={{height: 28, m: 0.5}} orientation="vertical"/>
                                    <IconButton color="primary" sx={{p: '10px'}} aria-label="directions"
                                                onClick={onAdd}>
                                        <AddIcon/>
                                    </IconButton>
                                </Paper>
                                {errorText()}
                            </Grid>
                        </Grid>
                    </Grid>
                    : <></>
                }
                <TryGuessGameContent game={props.gameModel} loading={loading} onSelect={onSelectWord} />
            </Grid>
    </React.Fragment>)
}

export default observer(TryGuessGame)