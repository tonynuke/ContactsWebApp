import * as React from 'react';
import { connect } from 'react-redux';
import * as EmployeesStore from '../store/EmployeesContainer';
import { ContactState, EmployeeState } from '../store/EmployeeState';
import { TextInput } from './TextInput';
import { DateInput } from './DateInput';
import { EmployeesProps } from './EmployeesProps';
import { Contacts } from './Contacts';
import { Container } from 'reactstrap';

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
                <Contacts {...this.props} />
            </React.Fragment >
        );
    }
}

export default connect(
    (state: EmployeeState) => state,
    EmployeesStore.actionCreators
)(Employee as any);
