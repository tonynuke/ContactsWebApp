import React, { FunctionComponent } from 'react';
import { connect } from 'react-redux';
import * as EmployeesStore from '../store/EmployeesContainer';
import { ContactState, EmployeeState } from '../store/EmployeeState';
import { TextInput } from './TextInput';
import { DateInput } from './DateInput';
import { EmployeesProps } from './EmployeesProps';
import { Contacts } from './Contacts';
import { Container } from 'reactstrap';

export const Employee: FunctionComponent<EmployeesProps> = (props) =>
    <React.Fragment>
        <Container>
            <TextInput handleChange={props.setName} title="Name" value={props.current.name} />
            <TextInput handleChange={props.setSurname} title="Surname" value={props.current.surname} />
            <TextInput handleChange={props.setPatronymic} title="Patronymic" value={props.current.patronymic} />
            <DateInput handleChange={props.setBirthDate} title="BirthDate" value={props.current.birthDate} />
            <TextInput handleChange={props.setOrganization} title="Organization" value={props.current.organization} />
            <TextInput handleChange={props.setPosition} title="Position" value={props.current.position} />
        </Container>
                Contacts
                <br />
        <Contacts {...props} />
    </React.Fragment>

export default connect(
    (state: EmployeeState) => state,
    EmployeesStore.actionCreators
)(Employee as any);
