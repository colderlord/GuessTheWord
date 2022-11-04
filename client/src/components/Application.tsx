import * as React from 'react';
import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';
import Typography from '@mui/material/Typography';
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import {observer} from 'mobx-react';
import {Storage} from "../storage/Storage";

import Rules from "./Rules";
import Game from "./Game";

const steps = ['Правила', 'Игра'];

export interface ApplicationProps{
    storage: Storage;
}

function getStepContent(step: number, storage: Storage) {
    switch (step) {
        case 0:
            return <Rules storage={storage}/>;
        case 1:
            return <Game storage={storage}/>;
        default:
            throw new Error('Unknown step');
    }
}

function Application(props: ApplicationProps) {
    const [activeStep, setActiveStep] = React.useState(0);

    const handleNext = () => {
        if (activeStep == steps.length - 1)
        {
            props.storage.clear();
            setActiveStep(0);
        }
        else
        {
            const check = props.storage.check();
            if (check == "") {
                props.storage.setGameModel(props.storage.currentGameInfo.uid);
                setActiveStep(activeStep + 1);
            } else {
                
            }
        }
    };

    return (
        <Container component="main" maxWidth="sm" sx={{ mb: 4 }}>
            <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
                <Typography component="h1" variant="h4" align="center">
                    Отгадай слово
                </Typography>
                <React.Fragment>
                    {getStepContent(activeStep, props.storage)}
                    <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                        <Button
                            variant="contained"
                            onClick={handleNext}
                            sx={{ mt: 3, ml: 1 }}
                        >
                            {activeStep === steps.length - 1 ? 'Завершить игру' : 'Начать игру'}
                        </Button>
                    </Box>
                </React.Fragment>
            </Paper>
        </Container>
    )
}

export default observer(Application)