import * as React from 'react';
import Button from "@mui/material/Button";
import {observer} from "mobx-react";
import {LetterModel, LetterType} from "../../../models/letter.interface";
import {WordModel} from "../../../models/word.interface";

export interface WordAnswerProps {
    word: WordModel;
    editable?: boolean;
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

function WordAnswer(props: WordAnswerProps) {
    function onClick(letter: LetterModel) {
        if (!props.editable) {
            return;
        }

        switch (letter.letterType) {
            case LetterType.None:
            {
                letter.setLetterType(LetterType.Any);
                break;
            }
            case LetterType.Any:
            {
                letter.setLetterType(LetterType.Fixed);
                break;
            }
            case LetterType.Fixed:
            {
                letter.setLetterType(LetterType.None);
                break;
            }
        }
    }
    
    return (
        <React.Fragment>
            {
                props.word.letters.map((letter: LetterModel) => (
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