import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as EmployeesStore from '../store/Employees';
import { Employee, EmployeesProps } from "./Employee";
import { EmployeeState } from "../store/EmployeeState";
import ReactModal from 'react-modal'

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
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => {
                        this.props.openNewModal();
                    }}>
                    Create new employee
                </button>
            </React.Fragment>
        );
    }

    private renderModalWindow() {
        if (!this.props.isModalOpen) {
            return (<div />);
        }
        return (<ReactModal
            isOpen={this.props.isModalOpen}
            contentLabel="Create new employee"
            ariaHideApp={false}>
            <Employee {...this.props} />
        </ReactModal>);
    }

    private ensureDataFetched() {
        this.props.requestEmployees();
    }

    private renderEmployeesTable() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Surname</th>
                        <th>Patronymic</th>
                        <th>Organization</th>
                        <th>Position</th>

                    </tr>
                </thead>
                <tbody>
                    {this.props.employees.map((employee: EmployeeState) =>
                        <tr key={employee.id}>
                            <td>{employee.id}</td>
                            <td>{employee.name}</td>
                            <td>{employee.surname}</td>
                            <td>{employee.patronymic}</td>
                            <td>{employee.organization}</td>
                            <td>{employee.position}</td>
                            <td>
                                <button type="button"
                                    className="btn btn-primary btn-lg"
                                    onClick={() => { this.props.openEditModal(employee); }}>
                                    Edit
                                </button>
                            </td>
                            <td>
                                <button type="button"
                                    className="btn btn-primary btn-lg"
                                    onClick={() => { this.props.deleteEmployee(employee.id); }}>
                                    Delete
                                </button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.employees, // Selects which state properties are merged into the component's props
    EmployeesStore.actionCreators, // Selects which action creators are merged into the component's props
)(Employees as any);
