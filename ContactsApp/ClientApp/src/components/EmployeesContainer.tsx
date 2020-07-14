import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesStore from '../store/EmployeesContainer';
import { Employee } from "./Employee";
import { EmployeeState } from "../store/EmployeeState";
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Collapse, CardBody, Card, Table } from 'reactstrap';

export type EmployeesProps =
    EmployeesStore.EmployeesState
    & typeof EmployeesStore.actionCreators

class Employees extends React.PureComponent<EmployeesProps> {
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                <h1>Employees</h1>
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
            <Modal isOpen={this.props.isModalOpen} toggle={() => this.props.closeModal(false)}>
                <ModalHeader toggle={() => this.props.closeModal(false)}>Edit employee</ModalHeader>
                <ModalBody>
                    <Employee {...this.props} />
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={() => this.props.saveEmployee(this.props.current)}>Save</Button>
                    <Button color="secondary" onClick={() => this.props.closeModal(false)}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }

    private ensureDataFetched() {
        this.props.requestEmployees();
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
    (state: ApplicationState) => state.employees, // Selects which state properties are merged into the component's props
    EmployeesStore.actionCreators, // Selects which action creators are merged into the component's props
)(Employees as any);
