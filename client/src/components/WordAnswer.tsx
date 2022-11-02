import * as React from 'react';
import Button from "@mui/material/Button";
import {WordModel} from "../interfaces/WordModel";
import {LetterModel, LetterType} from "../interfaces/LetterModel";

export interface WordAnswerProps {
    word: WordModel;
}

function getButtonColor(letter: LetterModel) {
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

export default function WordAnswer(props: WordAnswerProps) {

    return (
        <React.Fragment>
            {
                props.word.letters.map((letter: LetterModel) => (
                    <Button variant="contained" color={getButtonColor(letter)} sx={{ mt: 3, ml: 1 }}>
                        {letter.letter}
                    </Button>
                ))
            }
        </React.Fragment>
    )
}