import React, {Component} from "react";
import {Autocomplete, Box, TextField, Toolbar} from "@mui/material";
import {GuessGameSettings} from "../models/guessGameSettings";
import {observer} from "mobx-react";

export interface SettingsComponentProps {
    settings: GuessGameSettings
}

export interface SettingsComponentState {
    attemts: number
    wordLength: number
    lang: string
}

class SettingsComponent extends Component<SettingsComponentProps, SettingsComponentState> {
    constructor(props: SettingsComponentProps) {
        super(props);
        this.state = {
            attemts: this.props.settings.Attemts,
            wordLength: this.props.settings.WordLength,
            lang: this.props.settings.Language
        }
    }

    getSelectedValue() {
        return countries.find(v => {
            return v.lang === this.state.lang
        });
    }

    render() {
        return <>
            <Autocomplete
                id="lang-select"
                fullWidth
                options={countries}
                autoHighlight
                //onChange={e => this.setState({lang: e..target..value})}
                value={this.getSelectedValue()}
                getOptionLabel={(option) => option.label}
                renderOption={(props, option) => (
                    <Box component="li" sx={{ '& > img': { mr: 2, flexShrink: 0 } }} {...props}>
                        <img
                            loading="lazy"
                            width="20"
                            src={`https://flagcdn.com/w20/${option.code.toLowerCase()}.png`}
                            srcSet={`https://flagcdn.com/w40/${option.code.toLowerCase()}.png 2x`}
                            alt=""
                        />
                        {option.label} ({option.lang})
                    </Box>
                )}
                renderInput={(params) => (
                    <TextField
                        {...params}
                        label="Выберите язык"
                        inputProps={{
                            ...params.inputProps,
                            autoComplete: 'new-password', // disable autocomplete and autofill
                        }}
                    />
                )}
            />
            <TextField
                autoFocus
                inputProps={{ inputMode: 'numeric', pattern: '[0-9]*' }}
                margin="dense"
                id="attemts"
                label="Число попыток"
                fullWidth
                variant="standard"
                value={this.state.attemts}
                onChange={e => this.setState({attemts: Number(e.target.value)})}
                onBlur={e => this.props.settings.setAttemts(this.state.attemts)}
            />
            <TextField
                inputProps={{ inputMode: 'numeric', pattern: '[0-9]*' }}
                margin="dense"
                id="wordLength"
                label="Длина слова"
                fullWidth
                variant="standard"
                value={this.state.wordLength}
                onChange={e => this.setState({wordLength: Number(e.target.value)})}
                onBlur={e => this.props.settings.setWordLength(this.state.wordLength)}
            />
        </>;
    }
}

interface CountryType {
    code: string;
    lang: string;
    label: string;
}

const countries: readonly CountryType[] = [
    {
        code: 'RU',
        label: 'Russian Federation',
        lang: 'ru-RU'
    },
    {
        code: 'US',
        label: 'United States',
        lang: 'en-US'
    }
];

export default observer(SettingsComponent)