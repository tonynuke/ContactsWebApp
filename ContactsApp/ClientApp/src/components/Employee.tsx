import * as React from 'react';
import { connect } from 'react-redux';
import * as EmployeesStore from '../store/EmployeesContainer';
import { LinkState, EmployeeState } from '../store/EmployeeState';
import { TextInput } from './TextInput';
import { DateInput } from './DateInput';
import { Button, Container, Col, Row, Input, InputGroup, InputGroupAddon } from 'reactstrap';

export type EmployeesProps =
    EmployeesStore.EmployeesState // ... state we've requested from the Redux store
    & typeof EmployeesStore.actionCreators // ... plus action creators we've requested


export class Employee extends React.PureComponent<EmployeesProps> {
    render() {
        return (
            <React.Fragment>
                <Container>
                    <TextInput handleChange={this.props.setName} title="Name" value={this.props.current.name} />
                    <TextInput handleChange={this.props.setSurname} title="Surname" value={this.props.current.surname} />
                    <TextInput handleChange={this.props.setPatronymic} title="Patronymic" value={this.props.current.patronymic} />
                    <DateInput handleChange={this.props.setBirthDate} title="BirthDate" value={this.props.current.birthDate} />
                    <TextInput handleChange={this.props.setOrganization} title="Organization" value={this.props.current.organization} />
                    <TextInput handleChange={this.props.setPosition} title="Position" value={this.props.current.position} />
                </Container>
                Contacts
                <br />
                {this.renderLinksTable()}
                <br />
                <Button color="success"
                    onClick={() => { this.props.createLink("value", "type"); }}>
                    Create new contact
                </Button>
            </React.Fragment >
        );
    }

    private renderLinksTable() {
        return (
            <Container >
                {this.props.current.links.map((link: LinkState) =>
                    <Row>
                        <InputGroup>
                            <Input type="select" value={link.type} onChange={(event: React.ChangeEvent<HTMLInputElement>) => this.props.setLinkType(link.id, event.target.value)}>
                                <option>Skype</option>
                                <option>Email</option>
                            </Input>
                            <Input type="text" value={link.value} onChange={(event: React.ChangeEvent<HTMLInputElement>) => this.props.setLinkValue(link.id, event.target.value)} />
                            <InputGroupAddon addonType="append">
                                <Button color="danger"
                                    onClick={() => { this.props.deleteLink(link); }}>
                                    Delete
                                </Button>
                            </InputGroupAddon>
                        </InputGroup>
                    </Row>
                )}
            </Container>
        );
    }
}

export default connect(
    (state: EmployeeState) => state, // Selects which state properties are merged into the component's props
    EmployeesStore.actionCreators // Selects which action creators are merged into the component's props
)(Employee as any);
