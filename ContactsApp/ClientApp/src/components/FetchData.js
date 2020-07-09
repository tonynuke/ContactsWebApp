import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { employees: [], loading: true };
    }

    componentDidMount() {
        this.populateEmployeesData();
    }

    static renderEmployeesTable(employees) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map(employee =>
                        <tr key={employee.Id}>
                            <td>{employee.OrganisationName}</td>
                            <td>{employee.EmployeeName}</td>
                            <td>{employee.Position}</td>
                            <td>{employee.LinkValue}</td>
                            <td>{employee.LinkType}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderEmployeesTable(this.state.employees);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateEmployeesData() {
        const response = await fetch('employees');
        const data = await response.json();
        this.setState({ employees: data, loading: false });
    }
}
