import React, { FunctionComponent, useState } from 'react';
import { connect } from 'react-redux';
import * as EmployeesStore from '../store/EmployeesContainer';
import { EmployeeState, ContactType } from '../store/EmployeeState';
import Contacts from './Contacts';
import { ApplicationState } from '../store';
import { Container, Alert, Col, Label, Input, Row, Button } from 'reactstrap';

export type EmployeeProps =
    EmployeesStore.EmployeesState
    & typeof EmployeesStore.actionCreators


const Employee: FunctionComponent<EmployeeProps> = (props) => {
    const [nextContactId, setNextContactId] = useState(-1);

    const addNewContact = () => {
        props.createContact(nextContactId, "", ContactType.Other);
        setNextContactId(nextContactId - 1);
    };

    return (<React.Fragment>
        {props.current.errors.map((error, index) => {
            return <Alert key={index} color="danger">{error}</Alert>;
        })}
        <Container>
            <Row>
                <Col xs="3"><Label>Name</Label></Col>
                <Col xs="9">
                    <Input type="text" value={props.current.name}
                        onChange={(event) => props.updateEmployee(Object.assign({},
                            props.current,
                            { name: event.target.value }))} />
                </Col>
            </Row>
            <Row>
                <Col xs="3"><Label>Surname</Label></Col>
                <Col xs="9">
                    <Input type="text" value={props.current.surname}
                        onChange={(event) => props.updateEmployee(Object.assign({},
                            props.current,
                            { surname: event.target.value }))} />
                </Col>
            </Row>
            <Row>
                <Col xs="3"><Label>Patronymic</Label></Col>
                <Col xs="9">
                    <Input type="text" value={props.current.patronymic}
                        onChange={(event) => props.updateEmployee(Object.assign({},
                            props.current,
                            { patronymic: event.target.value }))} />
                </Col>
            </Row>
            <Row>
                <Col xs="3"><Label>Birth date</Label></Col>
                <Col xs="9">
                    <Input type="date" value={new Date(props.current.birthDate).toISOString().substr(0, 10)}
                        onChange={(event) => props.updateEmployee(Object.assign({},
                            props.current,
                            { birthDate: event.target.value }))} />
                </Col>
            </Row>
            <Row>
                <Col xs="3"><Label>Organization</Label></Col>
                <Col xs="9">
                    <Input type="text" value={props.current.organization}
                        onChange={(event) => props.updateEmployee(Object.assign({},
                            props.current,
                            { organization: event.target.value }))} />
                </Col>
            </Row>
            <Row>
                <Col xs="3"><Label>Position</Label></Col>
                <Col xs="9">
                    <Input type="text" value={props.current.position}
                        onChange={(event) => props.updateEmployee(Object.assign({},
                            props.current,
                            { position: event.target.value }))} />
                </Col>
            </Row>
        </Container>
        Contacts
        <br />
        <Contacts />
        <br />
        <Button color="success" onClick={addNewContact}>Create new contact</Button>
    </React.Fragment>
    );
}

export default connect(
    (state: ApplicationState) => state.employees,
    EmployeesStore.actionCreators
)(Employee as any);
