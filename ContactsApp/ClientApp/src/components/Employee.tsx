import * as React from 'react';
import { connect } from 'react-redux';
import * as EmployeesStore from '../store/Employees';
import { LinkState, EmployeeState } from '../store/EmployeeState';
import { TextInput } from './TextInput';

export type EmployeesProps =
    EmployeesStore.EmployeesState // ... state we've requested from the Redux store
    & typeof EmployeesStore.actionCreators // ... plus action creators we've requested


export class Employee extends React.PureComponent<EmployeesProps> {
    render() {
        return (
            <React.Fragment>
                <h2>Employee info</h2>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                    </thead>
                    <tbody>
                        <tr>
                            <TextInput
                                handleChange={this.props.setName}
                                title="Name"
                                value={this.props.current.name}
                            />
                        </tr>
                        <tr>
                            <TextInput
                                handleChange={() => { }}
                                title="Surname"
                                value={this.props.current.surname}
                            />
                        </tr>
                        <tr>
                            <TextInput
                                handleChange={this.props.setPosition}
                                title="Position"
                                value={this.props.current.position}
                            />
                        </tr>
                    </tbody>
                </table>
                <h2>Links</h2>
                {this.renderLinksTable()}
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => {
                        this.props.createLink("value", "type");
                    }}>
                    Create new link
                </button>
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => { this.props.saveEmployee(this.props.current); }}
                >
                    Save
                </button>
                <button type="button"
                    className="btn btn-primary btn-lg"
                    onClick={() => { this.props.closeModal(false); }}
                >
                    Close
                </button>
            </React.Fragment>
        );
    }

    private renderLinksTable() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Type</th>
                        <th>Value</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.current.links.map((link: LinkState) =>
                        <tr key={link.id}>
                            <td>{link.id}</td>
                            <td>{link.value}</td>
                            <td>{link.type}</td>
                            <td>{link.state}</td>
                            <td>
                                <button type="button"
                                    className="btn btn-primary btn-lg"
                                    onClick={() => { this.props.deleteLink(link); }}>
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
    (state: EmployeeState) => state, // Selects which state properties are merged into the component's props
    EmployeesStore.actionCreators // Selects which action creators are merged into the component's props
)(Employee as any);
