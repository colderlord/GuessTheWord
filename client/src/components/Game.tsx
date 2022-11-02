import * as React from 'react';
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import {WordModel} from "../interfaces/WordModel";
import {LetterType} from "../interfaces/LetterModel";
import WordAnswer from "./WordAnswer";
import InputWord from "./InputWord";

export interface GameInput {

}

const wordModels : WordModel[] = [
    {
        letters: [
            {
                letter: "в",
                letterType: LetterType.None,
                position: 0
            },
            {
                letter: "е",
                letterType: LetterType.Any,
                position: 1
            },
            {
                letter: "т",
                letterType: LetterType.None,
                position: 2
            },
            {
                letter: "к",
                letterType: LetterType.None,
                position: 3
            },
            {
                letter: "а",
                letterType: LetterType.None,
                position: 4
            },
        ]
    },
    {
        letters: [
            {
                letter: "б",
                letterType: LetterType.None,
                position: 0
            },
            {
                letter: "и",
                letterType: LetterType.None,
                position: 1
            },
            {
                letter: "с",
                letterType: LetterType.None,
                position: 2
            },
            {
                letter: "е",
                letterType: LetterType.Any,
                position: 3
            },
            {
                letter: "р",
                letterType: LetterType.Any,
                position: 4
            },
        ]
    },
    {
        letters: [
            {
                letter: "д",
                letterType: LetterType.None,
                position: 0
            },
            {
                letter: "р",
                letterType: LetterType.Any,
                position: 1
            },
            {
                letter: "е",
                letterType: LetterType.Fixed,
                position: 2
            },
            {
                letter: "й",
                letterType: LetterType.Any,
                position: 3
            },
            {
                letter: "ф",
                letterType: LetterType.Any,
                position: 4
            },
        ]
    }
]

export default function Game() {
    return(<React.Fragment>
            <Typography variant="h6" gutterBottom>
                Игра началась!
            </Typography>
            <Grid container>
                <Grid item xs={12}>
                    <InputWord />
                </Grid>
            {
                wordModels.map((word) => (
                    <Grid item xs={12}>
                        <WordAnswer word={word} />
                    </Grid>
                ))
            }
            </Grid>
    </React.Fragment>)
}
