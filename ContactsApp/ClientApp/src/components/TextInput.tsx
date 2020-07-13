import React, { FunctionComponent } from 'react';
import { Label, Input, Row, Col } from 'reactstrap';

export type TextInputProps = {
    handleChange: Function,
    title: string,
    value: string,
}

export const TextInput: FunctionComponent<TextInputProps> = ({ handleChange, title, value }) =>
    <Row>
        <Col xs="3">
            <Label>{title}</Label>
        </Col>
        <Col xs="9">
            <Input type="text" value={value} onChange={(event: React.ChangeEvent<HTMLInputElement>) => handleChange(event.target.value)} />
        </Col>
    </Row>