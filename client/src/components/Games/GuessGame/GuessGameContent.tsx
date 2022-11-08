import * as React from "react";
import {observer} from "mobx-react";
import Grid from "@mui/material/Grid";
import GuessGameModel from "../../../storage/Games/GuessGameModel";
import WordAnswer from "../../WordAnswer";

export interface GuessGameContentProps {
    game?: GuessGameModel;
    loading: boolean;
}

function GuessGameContent(props: GuessGameContentProps) {
    const game = props.game;
    if (game) {
        return (
            <React.Fragment>
                {
                    game.wordModel.map((word) => (
                        <Grid key={word.stringValue} item xs={12}>
                            <WordAnswer key={word.stringValue} word={word}/>
                        </Grid>
                    ))
                }
            </React.Fragment>
        )
    }
    return <></>
}


export default observer(GuessGameContent)