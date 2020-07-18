import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesStore from '../store/EmployeesContainer';
import { Searcher } from "./Searcher";
import Employee from "./Employee";
import { EmployeeState } from "../store/EmployeeState";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Table } from 'reactstrap';

export type EmployeesProps =
    EmployeesStore.EmployeesState
    & typeof EmployeesStore.actionCreators

class Employees extends React.PureComponent<EmployeesProps> {
    public componentDidMount() {
        this.props.requestEmployees(undefined);
    }

    public render() {
        return (
            <React.Fragment>
                <h1>Employees</h1>
                <Searcher search={(searchString: string) => this.props.requestEmployees(searchString)} />
                <br />
                {this.renderEmployeesTable()}
                {this.renderModalWindow()}
                <Button color="success"
                    onClick={() => { this.props.openNewModal(); }}>
                    Create new employee
                </Button>
            </React.Fragment>
        );
    }

    private renderModalWindow() {
        return (
            <Modal isOpen={this.props.isModalOpen} toggle={this.props.closeModal}>
                <ModalHeader toggle={this.props.closeModal}>Edit employee</ModalHeader>
                <ModalBody>
                    <Employee />
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={() => this.props.saveEmployee(this.props.current)}>Save</Button>
                    <Button color="secondary" onClick={this.props.closeModal}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }

    private renderEmployeesTable() {
        return (
            <Table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Surname</th>
                        <th>Patronymic</th>
                        <th>BirthDate</th>
                        <th>Organization</th>
                        <th>Position</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.employees.map((employee: EmployeeState) =>
                        <tr key={employee.id}>
                            <td>{employee.name}</td>
                            <td>{employee.surname}</td>
                            <td>{employee.patronymic}</td>
                            <td>{new Date(employee.birthDate).toLocaleDateString()}</td>
                            <td>{employee.organization}</td>
                            <td>{employee.position}</td>
                            <td>
                                <Button color="primary"
                                    onClick={() => { this.props.openEditModal(employee); }}>
                                    Edit
                                </Button>
                            </td>
                            <td>
                                <Button color="danger"
                                    onClick={() => { this.props.deleteEmployee(employee.id); }}>
                                    Delete
                                </Button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </Table>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.employees,
    EmployeesStore.actionCreators,
)(Employees as any);
