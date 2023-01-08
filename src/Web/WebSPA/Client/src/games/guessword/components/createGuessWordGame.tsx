import React, {Component} from "react";
import {observer} from "mobx-react";
import {Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle} from "@mui/material";
import {Add} from "@mui/icons-material";
import SettingsComponent from "./settings.component";
import {GuessGameSettings} from "../models/guessGameSettings";
import {guessWordService} from "../../../App";
import {GuessGame} from "../models/guessGame";

export interface CreateGuessWordGameProps {
    onGameCreated(game: GuessGame): void
}

export interface CreateGuessWordGameState {
    drawerOpened: boolean;
    loading: boolean;
    settings?: GuessGameSettings;
}

class CreateGuessWordGame extends Component<CreateGuessWordGameProps, CreateGuessWordGameState> {
    constructor(props: CreateGuessWordGameProps) {
        super(props);
        this.state = {
            loading: false,
            drawerOpened: false
        }
    }

    onAddClick() {
        this.setState({
            drawerOpened: true,
            settings: new GuessGameSettings()
        })
    }

    handleCloseDrawer() {
        this.setState({
            drawerOpened: false,
            settings: undefined
        })
    }

    async handleOk() {
        const settings = this.state.settings;
        let game : GuessGame | undefined;
        if (settings) {
            game = await guessWordService.createGame(settings);
        }
        this.handleCloseDrawer();
        if (game) {
            this.props.onGameCreated(game);
        }
    }

    handleCancel() {
        this.handleCloseDrawer();
    }

    render() {
        return (
            <>
                <Button
                    startIcon={<Add />}
                    onClick={_ => this.onAddClick()}
                >
                    Создать новую игру
                </Button>
                <Dialog open={this.state.drawerOpened} onClose={_ => this.handleCancel()}>
                    <DialogTitle>Создать игру</DialogTitle>
                    <DialogContent>
                        <DialogContentText>
                            Заполните настройки для создания игры.
                        </DialogContentText>
                        <SettingsComponent settings={this.state.settings ?? new GuessGameSettings()} />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={_ => this.handleOk()}>Ok</Button>
                        <Button onClick={_ => this.handleCancel()}>Отмена</Button>
                    </DialogActions>
                </Dialog>
            </>
        )
    }
}

export default observer(CreateGuessWordGame);