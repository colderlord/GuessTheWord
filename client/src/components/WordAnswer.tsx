import * as React from 'react';
import Button from "@mui/material/Button";
import {observer} from "mobx-react";
import {Word} from "../interfaces/Word";
import {Letter, LetterType} from "../interfaces/Letter";

export interface WordAnswerProps {
    word: Word;
}

function getButtonColor(letter: Letter) {
    switch (letter.letterType) {
        case LetterType.None:
        {
            return undefined;
        }
        case LetterType.Any:
        {
            return "warning";
        }
        case LetterType.Fixed:
        {
            return "success";
        }
        case LetterType.Default:
        {
            return "info";
        }
    }
}

function WordAnswer(props: WordAnswerProps) {
    return (
        <React.Fragment>
            {
                props.word.letters.map((letter: Letter) => (
                    <Button
                        key={"leter" + letter.letter + letter.position + props.word.stringValue}
                        variant="contained"
                        color={getButtonColor(letter)}
                        sx={{ mt: 3, ml: 1 }}
                    >
                        {letter.letter}
                    </Button>
                ))
            }
        </React.Fragment>
    )
}

export default observer(WordAnswer)