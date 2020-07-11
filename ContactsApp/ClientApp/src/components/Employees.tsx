import * as React from 'react';
import { connect } from 'react-redux';
import ReactModal from 'react-modal'
import { ApplicationState } from '../store';
import * as EmployeesStore from '../store/Employees';

type EmployeeProps =
    EmployeesStore.EmployeesState // ... state we've requested from the Redux store
    & typeof EmployeesStore.actionCreators; // ... plus action creators we've requested

class PopUp extends React.PureComponent<EmployeesProps> {
    render() {
        return (
            <div>
                <ReactModal
                    isOpen={this.props.isModalOpen}
                    contentLabel="Create new employee"
                    ariaHideApp={true}
                >
                    <button onClick={this.props.closeModal}>close</button>
                    <form>
                        <label>
                            Name:
                            <input type="text" value={this.props.current.name} name="name" />
                        </label>
                        <label>
                            Surname:
                            <input type="text" value={this.props.current.surname} name="surname" />
                        </label>
                        <label>
                            Position:
                            <input type="text" value={this.props.current.position} name="position" />
                        </label>
                    </form>
                    <button type="button"
                        className="btn btn-primary btn-lg"
                        onClick={() => { this.props.createEmployee("vasyan", "developer"); }}>
                        Save
                </button>
                </ReactModal>
            </div>
        );
    }
}

type EmployeesProps =
    EmployeesStore.EmployeesState // ... state we've requested from the Redux store
    & typeof EmployeesStore.actionCreators; // ... plus action creators we've requested


class Employees extends React.PureComponent<EmployeesProps> {
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Employees</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                {this.renderEmployeesTable()}
                <PopUp {...this.props} />
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => {
                        this.props.openCreateModal();
                    }}>
                    Create
                </button>
            </React.Fragment>
        );
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
                    {this.props.employees.map((employee: EmployeesStore.EmployeeState) =>
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
                                    onClick={() => { this.props.deleteEmployee([employee.id]); }}>
                                    Delete
                                </button>
                            </td>
                            <td>
                                <button type="button"
                                    className="btn btn-primary btn-lg"
                                    onClick={() => { this.props.openEditModal(employee); }}>
                                    Edit
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
    EmployeesStore.actionCreators // Selects which action creators are merged into the component's props
)(Employees as any);
