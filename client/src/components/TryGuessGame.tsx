import * as React from 'react';
import Grid from "@mui/material/Grid";
import {observer} from "mobx-react";
import {GameModel} from "../storage/Storage";
import WordAnswer from "./WordAnswer";

export interface TryGuessGameProps {
    game?: GameModel;
}

function TryGuessGame(props: TryGuessGameProps) {
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
export default observer(TryGuessGame)