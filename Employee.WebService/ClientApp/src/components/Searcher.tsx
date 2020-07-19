import React, { useState, FunctionComponent, useEffect } from 'react';
import { Tooltip, FormGroup, Form, Label, Input, InputGroup } from 'reactstrap';
import { FilterType } from '../store/EmployeesContainer';

export type SearcherProps = {
    search: Function
}

export const Searcher: FunctionComponent<SearcherProps> = ({ search }) => {
    const [tooltipOpen, setTooltipOpen] = useState(false);
    const [filter, setFilter] = useState('');
    const [type, setType] = useState(FilterType.Employee);

    useEffect(() => {
        search(filter, type);
    }, [filter, type]);

    const toggle = () => setTooltipOpen(!tooltipOpen);

    return (
        <React.Fragment>
            <Form inline>
                <FormGroup>
                    <InputGroup>
                        <Label className="mr-sm-2">Search</Label>
                        <Input type="select" value={type}
                            onChange={(event: React.ChangeEvent<HTMLInputElement>) => setType(FilterType[event.target.value as keyof typeof FilterType])}>
                            {Object.keys(FilterType).map(key => {
                                return <option key={key}>{key}</option>;
                            })}
                        </Input>
                        <Input type="text" placeholder="...details" id="Tooltip" value={filter}
                            onChange={(event: React.ChangeEvent<HTMLInputElement>) => setFilter(event.target.value)} />
                        <Tooltip placement="right" isOpen={tooltipOpen} target="Tooltip" toggle={toggle}>
                            Here are two search modes: 
                            <ul>
                                <li>Employee - get employee by their attributes</li>
                                <li>Contact - get employess by contact value</li>
                            </ul>
                    </Tooltip>
                    </InputGroup>
                </FormGroup>
            </Form>
        </React.Fragment>
    );
}