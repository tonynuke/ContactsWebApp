import React, { Component } from 'react';

export class Organizations extends Component {
    static displayName = Organizations.name;

    constructor(props) {
        super(props);
        this.state = {
            Organizations: [],
            OrganizationName: "unicorn",
            loading: true
        };
        this.addOrganization = this.addOrganization.bind(this);
    }

    async addOrganization() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name: this.state.OrganizationName })
        };

        const response = await fetch('employees/Organization', requestOptions);
        const data = await response.json();
        this.setState({
            Organizations: [...this.state.Organizations, { id: data, name: this.state.OrganizationName }]
        });
    }

    componentDidMount() {
        this.populateOrganizationsData();
    }

    static renderOrganizationsTable(Organizations) {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        {Organizations.map(Organization =>
                            <tr key={Organization.Id}>
                                <td>{Organization.Name}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <button className="btn btn-primary" onClick={this.addOrganization}>Add Organization</button>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Organizations.renderOrganizationsTable(this.state.Organizations);

        return (
            <div>
                <h1 id="tabelLabel">Organizations</h1>
                {contents}
            </div>
        );
    }

    async populateOrganizationsData() {
        const response = await fetch('odata/Organizationsquery');
        const data = await response.json();
        this.setState({ Organizations: data.value, loading: false });
    }
}
