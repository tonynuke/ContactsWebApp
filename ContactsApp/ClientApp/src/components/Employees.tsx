import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as EmployeesStore from '../store/Employees';

type EmployeesProps =
    EmployeesStore.EmployeesState // ... state we've requested from the Redux store
    & typeof EmployeesStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{ name: string, position: string }>; // ... plus incoming routing parameters


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
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => { this.props.createEmployee("vasyan", "developer"); }}>
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
                        <th>Action</th>
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
                            </button></td>
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
