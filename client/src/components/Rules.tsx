import * as React from 'react';
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";

export default function Rules() {
    return (
        <React.Fragment>
            <Typography variant="h6" gutterBottom>
                Правила игры
            </Typography>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <TextField
                        type={"number"}
                        id="lettersCount"
                        label="Количество букв"
                        defaultValue={5}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        type={"number"}
                        id="lettersCount"
                        label="Количество слов"
                        defaultValue={6}
                    />
                </Grid>
            </Grid>
        </React.Fragment>
    )
}