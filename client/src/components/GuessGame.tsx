import * as React from 'react';
import Grid from "@mui/material/Grid";
import {observer} from "mobx-react";
import WordAnswer from "./WordAnswer";
import GameModel from "../storage/Games/GameModel";

export interface GuessGameProps {
    game?: GameModel;
}

function GuessGame(props: GuessGameProps) {
    function gameContent() {
        if (props.game) {
            return props.game.wordModel.map((word) => (
                <Grid key={word.stringValue} item xs={12}>
                    <WordAnswer key={word.stringValue} word={word} />
                </Grid>
            ))
        }

        return<></>
    }

    return (<React.Fragment>
        {gameContent()}
    </React.Fragment>)
}
export default observer(GuessGame)