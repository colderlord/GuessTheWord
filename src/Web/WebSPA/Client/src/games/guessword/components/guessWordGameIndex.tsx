import React, {Component} from "react";
import {Box, Link, Toolbar} from "@mui/material";
import {observer} from "mobx-react";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import {guessWordService} from "../../../App";
import {Guessgameitem} from "../models/guessgameitem";
import {NavLink} from "react-router-dom";
import {FetchDataProps} from "../../../components/FetchData";

export interface GuessWordGameIndexProps {
    
}

export interface GuessWordGameIndexState {
    items: Guessgameitem[];
    loading: boolean;
}

class GuessWordGameIndex extends Component<GuessWordGameIndexProps, GuessWordGameIndexState> {
    constructor(props: GuessWordGameIndexProps) {
        super(props);
        this.state = { items: [], loading: true };
    }

    componentDidMount() {
        guessWordService.list(1, 15)
            .then(vv => {
                this.setState(
                    {
                        items: vv
                    }
                )
            })
        
    }

    render() {
        return <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
            <Toolbar />
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Id</TableCell>
                            <TableCell align="right">CreationDate</TableCell>
                            <TableCell align="right">StartDate</TableCell>
                            <TableCell align="right">EndDate</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {this.state.items.map((row) => (
                            <TableRow
                                key={row.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {row.id}
                                </TableCell>
                                <TableCell align="right">
                                    <NavLink
                                        exact
                                        to={"/guessGame/"+row.id}
                                        component={Link}
                                    >
                                        {row.creationDate.toLocaleString()}
                                    </NavLink>
                                </TableCell>
                                <TableCell align="right">{row.startDate?.toLocaleString()}</TableCell>
                                <TableCell align="right">{row.endDate?.toLocaleString()}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>;
    }
}

export default observer(GuessWordGameIndex);