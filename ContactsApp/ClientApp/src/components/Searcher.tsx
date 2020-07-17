import React, { useState, FunctionComponent } from 'react';
import { Tooltip, FormGroup, Form, Label, Input } from 'reactstrap';

export type SearcherProps = {
    search: Function
}


export const Searcher: FunctionComponent<SearcherProps> = ({search}) => {
    const [tooltipOpen, setTooltipOpen] = useState(false);
    const toggle = () => setTooltipOpen(!tooltipOpen);

    return (
        <React.Fragment>
            <Form inline>
                <FormGroup>
                    <Label className="mr-sm-2">Search</Label>
                    <Input type="text" placeholder="Search" id="Tooltip"
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => search(event.target.value)} />
                    <Tooltip placement="right" isOpen={tooltipOpen} target="Tooltip" toggle={toggle}>
                        Search using employee fields
                    </Tooltip>
                </FormGroup>
            </Form>
        </React.Fragment>
    );
}