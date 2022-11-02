import * as React from 'react';
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";

export interface InputWordProps {
    lettersNumber?: number
}

export default function InputWord() {
    return(
        <React.Fragment>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        id="wordInput"
                        label="Введите ответ"
                    />
                </Grid>
            </Grid>
        </React.Fragment>
    )
}