import React, { Component } from 'react';

export class Organisations extends Component {
    static displayName = Organisations.name;

    constructor(props) {
        super(props);
        this.state = {
            organisations: [],
            organisationName: "unicorn",
            loading: true
        };
        this.addOrganisation = this.addOrganisation.bind(this);
        //this.addOrganisation();
    }

    async addOrganisation() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name: this.state.organisationName })
        };

        const response = await fetch('employees/organisation', requestOptions);
        const data = await response.json();
        this.setState({
            organisations: [...this.state.organisations, { id: data, name: this.state.organisationName }]
        });
    }

    componentDidMount() {
        this.populateOrganisationsData();
    }

    static renderOrganisationsTable(organisations) {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        {organisations.map(organisation =>
                            <tr key={organisation.Id}>
                                <td>{organisation.Name}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <button className="btn btn-primary" onClick={this.addOrganisation}>Add organisation</button>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Organisations.renderOrganisationsTable(this.state.organisations);

        return (
            <div>
                <h1 id="tabelLabel">Organisations</h1>
                {contents}
            </div>
        );
    }

    async populateOrganisationsData() {
        const response = await fetch('odata/organisationsquery');
        const data = await response.json();
        this.setState({ organisations: data.value, loading: false });
    }
}
