import React, {Component} from "react";
import {observer} from "mobx-react";
import {Paper, TextField} from "@mui/material";
import { WordModel} from "../../../models/word.interface";
import {LetterModel, LetterType} from "../../../models/letter.interface";

export interface WordItemProps {
    wordLength: number
    disabled: boolean
    word?: string
}

export interface WordItemState {
    focused: number
    wordModel: WordModel
}

class WordItem extends Component<WordItemProps, WordItemState> {
    private regExp = new RegExp('[а-я]', 'i')
    private inputs : HTMLDivElement[] | null[] = [];

    constructor(props: WordItemProps) {
        super(props);
        const word = new WordModel();
        for (let i = 0; i < props.wordLength; i++) {
            word.letters.push(new LetterModel('', LetterType.Default, i))
        }
        this.state = { focused: 0, wordModel: word };
    }

    componentDidMount() {
        this.inputs[0]?.focus();
    }

    onFocus(index: number) {
        this.setState(
            {
                focused: index
            }
        )
    }

    onChangeLetter(value: string, index: number) {
        if (!value) {
            const ww = this.state.wordModel;
            ww.letters[index].setLetter(value);
            this.setState(
                {
                    focused: index === 0 ? index : index - 1
                }
            )
            return;
        }

        const ww = this.state.wordModel;
        if (value.length > 1 || !value.match(this.regExp)) {
            ww.letters[index].setLetter('');
            return;
        }

        ww.letters[index].setLetter(value);
        this.setState(
            {
                focused: index === ww.letters.length ? index :index + 1
            }
        )

        const input = this.inputs[index + 1];
        if (input) {
            (input.children[0].children[0] as HTMLInputElement).focus();
        }
    }

    render() {
        const { wordLength, disabled } = this.props;
        let letters = [];
        for (let i = 0; i < wordLength; i++) {
            letters.push(
                <TextField
                    key={i.toString()}
                    focused={!disabled && this.state.focused === i}
                    autoFocus={!disabled && this.state.focused === i}
                    onFocus={e => this.onFocus(i)}
                    ref={(input) => { this.inputs[i] = input; }}
                    disabled={disabled}
                    size="small"
                    InputProps={{
                        style: { width: `40px` },
                    }}
                    onChange={e => this.onChangeLetter(e.currentTarget.value, i)}
                    value={this.state.wordModel.letters[i].letter}
                />
            )
        }
        return (
            <Paper
                elevation={0}
                sx={{ p: '2px 4px', display: 'flex', alignItems: 'center', width: 400 }}
            >
                {letters}
            </Paper>
        );
    }
}

export default observer(WordItem)