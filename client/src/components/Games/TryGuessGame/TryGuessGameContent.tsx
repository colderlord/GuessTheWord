import * as React from "react";
import {observer} from "mobx-react";
import Grid from "@mui/material/Grid";
import List from "@mui/material/List";
import ListItemText from "@mui/material/ListItemText";
import CircularProgress from "@mui/material/CircularProgress";
import ListItemButton from "@mui/material/ListItemButton";
import IconButton from "@mui/material/IconButton";
import AddIcon from "@mui/icons-material/Add";
import Divider from "@mui/material/Divider";

import WordAnswer from "../../WordAnswer";
import TryGuessGameModel from "../../../storage/Games/TryGuessGameModel";
import {WordModel} from "../../../storage/WordModel";

export interface TryGuessGameContentProps {
    game: TryGuessGameModel;
    loading: boolean;
    onSelect(word: WordModel) : void;
}

function TryGuessGameContent(props: TryGuessGameContentProps) {
    const [selectedWord, setWord] = React.useState<WordModel | undefined>(undefined);
    const [confirmedWords, setConfirmedWords] = React.useState<WordModel[]>([]);

    function onWordClick(word: string) {
        const w = new WordModel();
        w.setStringValue(word, true);
        setWord(w);
        props.game.SetAnswers([]);
    }

    function confirm(word: WordModel) {
        setWord(undefined);
        setConfirmedWords(oldArray => [...oldArray, word]);
        props.onSelect(word);
    }

    function gameContent() {
        let progressContent;
        if (props.loading) {
            progressContent = <Grid container>
                <Grid item xs={12}>
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <CircularProgress/>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>;
        }
        const answersContent = <><Grid container>
            <Grid item xs={12}>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        {selectedWord
                            ? <>
                                <WordAnswer word={selectedWord} editable />
                                <IconButton color="primary" sx={{ mt: 3, ml: 1 }} aria-label="directions" onClick={() => confirm(selectedWord)}>
                                    <AddIcon />
                                </IconButton>
                                <Divider orientation="horizontal" />
                                </>
                            : <></>
                        }
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
        <Grid container>
            <Grid item xs={12}>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        {
                            confirmedWords.reverse().map((w) => {
                                return (
                                    <>
                                        <WordAnswer word={w}/>
                                        <Divider orientation="horizontal" />
                                    </>
                                );
                            })
                        }
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
        </>
        
        const answersListContent = <Grid container>
            <Grid item xs={12}>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                <List dense>
                    {
                        props.game.answers.map((answer) => (
                            <ListItemButton onClick={() => onWordClick(answer)}>
                                <ListItemText primary={answer}/>
                            </ListItemButton>
                        ))
                    }
                </List>
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
        return <React.Fragment>
            {
                props.game.wordModel.map((word) => (
                    <Grid key={word.stringValue} item xs={12}>
                        <WordAnswer key={word.stringValue} word={word}/>
                    </Grid>
                ))
            }
            {progressContent}
            {answersContent}
            {answersListContent}
        </React.Fragment>
    }

    return (<React.Fragment>
        {gameContent()}
    </React.Fragment>)
}

export default observer(TryGuessGameContent)