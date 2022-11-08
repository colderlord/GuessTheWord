import * as React from 'react';
import {observer} from "mobx-react";
import Grid from "@mui/material/Grid";

import WordsGameModel from "../../../storage/Games/WordsGameModel";
import WordAnswer from "../../WordAnswer";

export interface WordsGameContentProps {
    game: WordsGameModel;
    loading: boolean;
}

function WordsGameContent(props: WordsGameContentProps) {
    return (<React.Fragment>
        <React.Fragment>
            {
                props.game.wordModel.slice().reverse().map((word) => (
                    <Grid key={word.stringValue} item xs={12}>
                        <WordAnswer key={word.stringValue} word={word}/>
                    </Grid>
                ))
            }
        </React.Fragment>
    </React.Fragment>)
}
export default observer(WordsGameContent)