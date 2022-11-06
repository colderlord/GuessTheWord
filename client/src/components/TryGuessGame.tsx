import * as React from "react";
import Grid from "@mui/material/Grid";
import List from "@mui/material/List";
import ListItemText from "@mui/material/ListItemText";
import WordAnswer from "./WordAnswer";
import GameModel from "../storage/Games/GameModel";
import {ListItem} from "@mui/material";
import {observer} from "mobx-react";

export interface TryGuessGameProps {
    game?: GameModel;
}

function TryGuessGame(props: TryGuessGameProps) {
    function gameContent() {
        const game = props.game;
        if (game) {
            return <React.Fragment>
                {
                    game.wordModel.map((word) => (
                        <Grid key={word.stringValue} item xs={12}>
                            <WordAnswer key={word.stringValue} word={word}/>
                        </Grid>
                    ))
                }
                <Grid item xs={12} md={6}>
                    <List dense>
                        {
                            game.answers.map((answer) => (
                                <ListItem>
                                    <ListItemText primary={answer} />
                                </ListItem>
                            ))
                        }
                    </List>
                </Grid>
            </React.Fragment>
        }

        return<></>
    }

    return (<React.Fragment>
        {gameContent()}
    </React.Fragment>)
}

export default observer(TryGuessGame)