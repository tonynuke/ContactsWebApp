import React, { FunctionComponent } from 'react';
import { InputGroup, Label, Input, Row, Col } from 'reactstrap';

export type DateInputProps = {
    handleChange: Function,
    title: string,
    value: Date,
}

export const DateInput: FunctionComponent<DateInputProps> = ({ handleChange, title, value }) =>
    <Row>
        <Col xs="3">
            <Label>{title}</Label>
        </Col>
        <Col xs="9">
            <Input type="date" value={new Date(value).toISOString().substr(0, 10)} onChange={(event: React.ChangeEvent<HTMLInputElement>) => handleChange(event.target.value)} />
        </Col>
    </Row>