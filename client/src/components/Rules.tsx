import * as React from 'react';
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import { observer } from 'mobx-react';

import { Settings } from "../interfaces/Settings"

export interface RulesProps {
    settings: Settings;
}

function Rules(props: RulesProps) {
    function onChangeLettersCount(e: any) {
        var value = e.currentTarget.value;
        if (value == undefined) {
            return;
        }

        var numberValue = Number(value);
        if (numberValue == 0 || numberValue < 0) {
            return;
        }

        props.settings.SetLettersCount(numberValue)
    }

    function onChangeAttempts(e: any) {
        var value = e.currentTarget.value;
        if (value == undefined) {
            return;
        }

        var numberValue = Number(value);
        if (numberValue == 0 || numberValue < 0) {
            return;
        }

        props.settings.SetAttemptsCount(numberValue)
    }

    return (
        <React.Fragment>
            <Typography variant="h6" gutterBottom>
                Правила игры
            </Typography>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <TextField
                        type={"number"}
                        id="lettersCount"
                        label="Количество букв"
                        value={props.settings.LettersCount}
                        onChange={onChangeLettersCount}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        type={"number"}
                        id="lettersCount"
                        label="Количество слов"
                        value={props.settings.Attempts}
                        onChange={onChangeAttempts}
                    />
                </Grid>
            </Grid>
        </React.Fragment>
    )
}

export default observer(Rules);