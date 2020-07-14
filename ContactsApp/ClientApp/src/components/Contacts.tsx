import React, { FunctionComponent } from 'react';
import { connect } from 'react-redux';
import * as EmployeesStore from '../store/EmployeesContainer';
import { ContactState, ContactType } from '../store/EmployeeState';
import { Button, Container, Row, Input, InputGroup, InputGroupAddon } from 'reactstrap';

export type ContactsProps =
    EmployeesStore.EmployeesState
    & typeof EmployeesStore.actionCreators


export const Contacts: FunctionComponent<ContactsProps> = (props) =>
    <React.Fragment>
        <Container >
            {props.current.contacts.map((contact: ContactState) =>
                <Row>
                    <InputGroup>
                        <Input type="select" value={contact.type} onChange={
                            (event: React.ChangeEvent<HTMLInputElement>) => props.setContactType(contact.id, ContactType[event.target.value as keyof typeof ContactType])}>
                            {Object.keys(ContactType).map(key => {
                                return <option>{key}</option>
                            })}
                        </Input>
                        <Input valid={contact.isValid} invalid={!contact.isValid} type="text" value={contact.value} onChange={
                            (event: React.ChangeEvent<HTMLInputElement>) => props.setContactValue(contact.id, event.target.value)} />
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
        <br />
        <Button color="success"
            onClick={() => { props.createContact("...", ContactType.Email); }}>
            Create new contact
                </Button>
    </React.Fragment>

export default connect(
    (state: ContactState) => state,
    EmployeesStore.actionCreators
)(Contacts as any);
