import * as React from 'react';
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";
import {observer} from 'mobx-react';
import {Storage} from "../storage/Storage";
import {GameInfo} from "../interfaces/GameInfo";

export interface RulesProps {
    storage: Storage
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

        props.storage.settings.SetLettersCount(numberValue)
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

        props.storage.settings.SetAttemptsCount(numberValue)
    }

    function onChangeGame(e: GameInfo) {
        props.storage.setCurrentGame(e);
    }

    return (
        <React.Fragment>
            <Typography variant="h6" gutterBottom>
                Правила игры
            </Typography>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Autocomplete
                        id="game-selector"
                        value={props.storage.currentGameInfo}
                        onChange={(event: any, newValue: GameInfo) => {
                            onChangeGame(newValue);
                        }}
                        defaultValue={props.storage.currentGameInfo}
                        disableClearable={true}
                        loading={props.storage.gameInfosStorage.loadingInfos}
                        options={props.storage.gameInfosStorage.gameInfos}
                        getOptionLabel={(option) => option.name}
                        onOpen={async () => {
                            await props.storage.gameInfosStorage.getGameInfosAsync();
                        }}
                        renderInput={(params) => {
                            return (<TextField {...params} label="Выберите игру" />);
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        type={"number"}
                        id="lettersCount"
                        label="Количество букв"
                        value={props.storage.settings.lettersCount}
                        onChange={onChangeLettersCount}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        type={"number"}
                        id="lettersCount"
                        label="Количество слов"
                        value={props.storage.settings.attempts}
                        onChange={onChangeAttempts}
                    />
                </Grid>
            </Grid>
        </React.Fragment>
    )
}

export default observer(Rules);