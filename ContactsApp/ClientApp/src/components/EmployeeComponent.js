import React, { Component } from 'react';
import { Button, Table, Input } from 'reactstrap';

export class LinkViewModel {
    constructor(id, name, type) {
        this.Id = id;
        this.Name = name;
        this.Type = type;

        this.IsSelected = false;
    }

    select() {
        this.IsSelected = !this.IsSelected;
    }
}

export class EmployeeViewModel {
    constructor(id, name, surname, patronymic, birthDate, organization, position) {
        this.Id = id;
        this.Name = name;
        this.Surname = surname;
        this.Patronymic = patronymic;
        this.BirthDate = birthDate;

        this.Organization = organization;
        this.Position = position;

        this.Links = [];

        this.IsSelected = false;

        this.select = this.select.bind(this);
    }

    select() {
        this.IsSelected = !this.IsSelected;
    }
}

export class EmployeeComponent extends Component {
    static displayName = EmployeeComponent.name;

    constructor(props) {
        super(props);
        this.state = {
            employees: [],
            newOrganizationName: 'unicorn'
        };
        this.addOrganization = this.addOrganization.bind(this);
        this.deleteOrganization = this.deleteOrganization.bind(this);
    }

    async deleteOrganization() {
        const ids = this.state.organizations
            .filter(organisation => organisation.IsSelected)
            .map(organisation => { return organisation.Id; });

        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ids: ids })
        };

        const response = await fetch('organizations', requestOptions);

        if (response.status == 200) {
            this.setState({
                organizations: this.state.organizations.filter(organisation => organisation.IsSelected)
            });
        }
    }

    async addOrganization() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name: this.state.organizationName })
        };

        const response = await fetch('organizations', requestOptions);
        const data = await response.json();
        this.setState({
            organizations: [...this.state.organizations, new OrganizationViewModel(data, this.state.newOrganizationName)]
        });
    }

    async populateOrganizationsData() {
        const response = await fetch('odata/organizations');
        const data = await response.json();
        const viewModel = data.value.map(organization => {
            return new OrganizationViewModel(organization.Id, organization.Name);
        });
        this.setState({ organizations: viewModel, loading: false });
    }

    renderOrganizationsTable() {
        return (
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>IsSelected</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.organizations.map(organization =>
                        <tr key={organization.Id}>
                            <td>{organization.Id}</td>
                            <td>{organization.Name}</td>
                            <td>
                                <Input type="checkbox" idTag={organization.Id} onClick={organization.select} />
                            </td>
                        </tr>
                    )}
                </tbody>
            </Table >
        );
    }

    componentDidMount() {
        this.populateOrganizationsData();
    }

    render() {
        let contents = this.renderOrganizationsTable();

        return (
            <div>
                <h1 id="tabelLabel">Organization</h1>
                {contents}
                <Button onClick={this.addOrganization}>+</Button>
                <Button onClick={this.deleteOrganization}>-</Button>
            </div>
        );
    }
}
