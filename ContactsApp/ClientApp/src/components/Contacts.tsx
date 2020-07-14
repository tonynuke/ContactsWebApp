import * as React from 'react';
import { connect } from 'react-redux';
import * as EmployeesStore from '../store/EmployeesContainer';
import { ContactState, EmployeeState } from '../store/EmployeeState';
import { Button, Container, Row, Input, InputGroup, InputGroupAddon } from 'reactstrap';
import { EmployeesProps } from './EmployeesProps';

export class Contacts extends React.PureComponent<EmployeesProps> {
    render() {
        return (
            <React.Fragment>
                <Container >
                    {this.props.current.contacts.map((contact: ContactState) =>
                        <Row>
                            <InputGroup>
                                <Input type="select" value={contact.type} onChange={
                                    (event: React.ChangeEvent<HTMLInputElement>) => this.props.setContactType(contact.id, event.target.value)}>
                                    <option>Skype</option>
                                    <option>Email</option>
                                </Input>
                                <Input type="text" value={contact.value} onChange={
                                    (event: React.ChangeEvent<HTMLInputElement>) => this.props.setContactValue(contact.id, event.target.value)} />
                                <InputGroupAddon addonType="append">
                                    <Button color="danger"
                                        onClick={() => { this.props.deleteContact(contact.id); }}>
                                        Delete
                                    </Button>
                                </InputGroupAddon>
                            </InputGroup>
                        </Row>
                    )}
                </Container>
                <br />
                <Button color="success"
                    onClick={() => { this.props.createContact("...", "Email"); }}>
                    Create new contact
                </Button>
            </React.Fragment >
        );
    }
}

export default connect(
    (state: ContactState) => state,
    EmployeesStore.actionCreators
)(Contacts as any);
