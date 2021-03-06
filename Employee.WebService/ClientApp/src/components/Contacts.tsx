﻿import React, { FunctionComponent } from 'react';
import { connect } from 'react-redux';
import * as EmployeesStore from '../store/EmployeesContainer';
import { actionCreators } from '../store/EmployeeActions';
import { ApplicationState } from '../store';
import { ContactType } from '../store/EmployeeState';
import { Button, Container, Row, Input, InputGroup, InputGroupAddon } from 'reactstrap';

export type ContactsProps =
    EmployeesStore.EmployeesState
    & typeof actionCreators


const Contacts: FunctionComponent<ContactsProps> = (props) =>
    <React.Fragment>
        <Container >
            {props.current.contacts.map(contact =>
                <Row key={contact.id}>
                    <InputGroup>
                        <Input type="select" value={contact.type}
                            onChange={(event) => props.updateContact(
                                Object.assign({}, contact, { type: event.target.value }))}>
                            {Object.keys(ContactType).map(key => {
                                return <option key={key}>{key}</option>;
                            })}
                        </Input>
                        <Input valid={contact.isValid} invalid={!contact.isValid} type="text" value={contact.value}
                            onChange={(event) => props.updateContact(Object.assign({},
                                contact,
                                { value: event.target.value }))} />
                        <InputGroupAddon addonType="append">
                            <Button color="danger"
                                onClick={() => { props.deleteContact(contact.id); }}>
                                Delete
                            </Button>
                        </InputGroupAddon>
                    </InputGroup>
                </Row>
            )}
        </Container>
    </React.Fragment>;

export default connect(
    (state: ApplicationState) => state.employees,
    actionCreators
)(Contacts as any);
