import React, { useState, FunctionComponent } from 'react';
import * as EmployeesStore from '../store/EmployeesContainer';
import { ContactState, ContactType } from '../store/EmployeeState';
import { Tooltip, FormGroup, Form, Label, Input } from 'reactstrap';

export type Searcher =
    EmployeesStore.EmployeesState
    & typeof EmployeesStore.actionCreators


export const Searcher: FunctionComponent<Searcher> = (props) => {
    const [tooltipOpen, setTooltipOpen] = useState(false);

    const toggle = () => setTooltipOpen(!tooltipOpen);

    return (
        <React.Fragment>
            <Form inline>
                <FormGroup>
                    <Label className="mr-sm-2">Search</Label>
                    <Input type="text" placeholder="Search" id="Tooltip"
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => props.requestEmployees(event.target.value)} />
                    <Tooltip placement="right" isOpen={tooltipOpen} target="Tooltip" toggle={toggle}>
                        Search using employee fields
                    </Tooltip>
                </FormGroup>
            </Form>
        </React.Fragment>
    );
}