import * as React from 'react';
import {observer} from "mobx-react";
import Typography from "@mui/material/Typography";

import {Storage} from "../../../storage/Storage";

export interface WordsGameProps {
    storage: Storage;
}

function WordsGame(props: WordsGameProps) {

    return (<React.Fragment>
        <Typography variant="h6" gutterBottom>
                Игра НЕ СДЕЛАНА!
        </Typography>
    </React.Fragment>)
}
export default observer(WordsGame)