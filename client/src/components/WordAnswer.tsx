import * as React from 'react';
import Button from "@mui/material/Button";
import {observer} from "mobx-react";
import {Word} from "../interfaces/Word";
import {Letter, LetterType} from "../interfaces/Letter";

export interface WordAnswerProps {
    word: Word;
    editable?: boolean;
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
    function onClick(letter: Letter) {
        if (!props.editable) {
            return;
        }

        switch (letter.letterType) {
            case LetterType.None:
            {
                letter.letterType = LetterType.Any;
                break;
            }
            case LetterType.Any:
            {
                letter.letterType = LetterType.Fixed;
                break;
            }
            case LetterType.Fixed:
            {
                letter.letterType = LetterType.None;
                break;
            }
        }
    }
    
    return (
        <React.Fragment>
            {
                props.word.letters.map((letter: Letter) => (
                    <Button
                        key={"leter" + letter.letter + letter.position + props.word.stringValue}
                        variant="contained"
                        color={getButtonColor(letter)}
                        onClick={() => onClick(letter)}
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