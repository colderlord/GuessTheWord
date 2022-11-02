import * as React from 'react';
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import InputBase from "@mui/material/InputBase";
import Divider from "@mui/material/Divider";
import AddIcon from '@mui/icons-material/Add';
import IconButton from '@mui/material/IconButton';
import WordAnswer from "./WordAnswer";
import {Storage} from "../storage/Storage";

export interface GameProps {
    storage: Storage;
}

export default function Game(props: GameProps) {
    const [value, setValue] = React.useState<string>("");
    function onAdd() {
       props.storage.AddWord(value);
       setValue("");
    }

    return(<React.Fragment>
            <Typography variant="h6" gutterBottom>
                Игра началась!
            </Typography>
            <Grid container>
                <Grid item xs={12}>
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <Paper
                                component="form"
                                sx={{ p: '2px 4px', display: 'flex', alignItems: 'center', width: 400 }}
                            >
                                <InputBase
                                    sx={{ ml: 1, flex: 1 }}
                                    placeholder="Введите ответ"
                                    inputProps={{ 'aria-label': 'Введите ответ' }}
                                    value={value}
                                    onChange={e => setValue(e.currentTarget.value)}
                                />
                                <Divider sx={{ height: 28, m: 0.5 }} orientation="vertical" />
                                <IconButton color="primary" sx={{ p: '10px' }} aria-label="directions" onClick={onAdd}>
                                    <AddIcon />
                                </IconButton>
                            </Paper>
                        </Grid>
                    </Grid>
                </Grid>
            {
                props.storage.WordModel.map((word) => (
                    <Grid item xs={12}>
                        <WordAnswer word={word} />
                    </Grid>
                ))
            }
            </Grid>
    </React.Fragment>)
}
