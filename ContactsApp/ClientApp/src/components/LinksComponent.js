import React, { Component } from 'react';
import PropTypes from 'prop-types'
import { Button, Table, Input } from 'reactstrap';

export class LinkViewModel {
    constructor(id, name, type) {
        this.id = id;
        this.name = name;
        this.type = type;

        this.isSelected = false;
    }

    select() {
        this.isSelected = !this.isSelected;
    }
}

export class LinksComponent extends Component {
    static displayName = EmployeeComponent.name;

    constructor(props) {
        super(props);
        this.state = {
            employee: this.props.employee
        };

        this.createLink = this.createLink.bind(this);
        this.updateLink = this.updateLink.bind(this);
        this.deleteLink = this.deleteLink.bind(this);
    }

    async deleteLink() {
        const ids = this.state.employee.links
            .filter(link => link.isSelected)
            .map(link => { return link.id; });

        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ids: ids })
        };

        const response = await fetch('links', requestOptions);

        if (response.status == 200) {
            this.setState(
                { organizations: this.state.employee.links.filter(link => link.IsSelected) }
            );
        }
    }

    async createLink() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name: this.state.organizationName })
        };

        const response = await fetch('organizations', requestOptions);
        const data = await response.json();
        this.setState({
            employee.links: [...this.state.employee.links, new LinkViewModel(data, this.state.newOrganizationName)]
        });
    }

    renderLinksTable() {
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
                    {this.state.employee.links.map(organization =>
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

    //componentDidMount() {
    //    this.populateOrganizationsData();
    //}

    render() {
        let contents = this.renderLinksTable();

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
