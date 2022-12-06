import * as React from 'react';
import {observer} from "mobx-react";
import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import InputBase from "@mui/material/InputBase";
import Divider from "@mui/material/Divider";
import IconButton from "@mui/material/IconButton";
import AddIcon from "@mui/icons-material/Add";

import WordsGameModel from "../../../storage/Games/WordsGameModel";
import WordsGameContent from "./WordsGameContent";
import FormHelperText from "@mui/material/FormHelperText";

export interface WordsGameProps {
    gameModel: WordsGameModel;
}

function WordsGame(props: WordsGameProps) {
    const [loading, setLoading] = React.useState<boolean>(false);
    const [error, setError] = React.useState<string>("");
    const [value, setValue] = React.useState<string>("");

    async function onAdd() {
        const settings = props.gameModel.settings;
        const gameModel = props.gameModel;
        if (gameModel) {
            setLoading(true);
            try {
                await gameModel.AddWord(value);
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
            <WordsGameContent game={props.gameModel} loading={loading} />
        </Grid>
    </React.Fragment>)
}
export default observer(WordsGame)