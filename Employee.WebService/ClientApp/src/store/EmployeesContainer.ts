import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import * as Employee from "./EmployeeActions";
import { EmployeeState } from "./EmployeeState";
import * as EmployeeReducer from "./EmployeeReducer";
import * as Client from "../services/OpenAPI";

export enum FilterType {
    Employee = "Employee",
    Contact = "Contact",
}

export interface EmployeesState {
    employees: EmployeeState[];
    isModalOpen: boolean;

    current: EmployeeState;
}

export interface OpenEditModalAction {
    type: 'OPEN_EDIT_MODAL';
    employee: EmployeeState;
}

export interface CloseEditModalAction {
    type: 'CLOSE_EDIT_MODAL';
}

export interface DeleteEmployeeAction {
    type: 'DELETE_EMPLOYEE';
    id: number;
}

export interface AddEmployeeAction {
    type: 'ADD_EMPLOYEE';
    employee: EmployeeState;
}

interface ReceiveEmployeesAction {
    type: 'RECEIVE_EMPLOYEES';
    employees: EmployeeState[];
}

export type KnownAction = ReceiveEmployeesAction | OpenEditModalAction | CloseEditModalAction
    | DeleteEmployeeAction | AddEmployeeAction
    | Employee.KnownAction;


export const actionCreators = {
    saveEmployee: (employee: EmployeeState): AppThunkAction<KnownAction> => async (dispatch, getState) => {
        //if (employee.errors.length > 0)
        //    return;
        const newEmployeeId = -1;
        const isNewEmployee = employee.id === newEmployeeId;
        const client: Client.Client = new Client.Client();
        let isSuccess: boolean = false;
        if (isNewEmployee) {
            try {
                let newId = await client.createEmployee(new Client.EmployeeDTO({ name: employee.name }));
                dispatch({ type: "ADD_EMPLOYEE", employee: Object.assign({}, employee, { id: newId }) });
                isSuccess = true;
            } catch (e) {
                const exception = await e;
                console.log(exception);
            }
        } else {
            try {
            let response = await client.updateEmployee(employee.id, new Client.CreateEmployeeDTO({ name: employee.name }));
            dispatch({ type: "ADD_EMPLOYEE", employee: employee });
                isSuccess = true;
            } catch (e) {
                const exception = await e;
                console.log(exception);
            }
        }

        if (isSuccess) {
            dispatch({ type: "CLOSE_EDIT_MODAL" });
        }
    },
    requestEmployees: (filter: string | undefined, type: FilterType): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        let odataUrl = `employees`;
        if (filter) {
            if (type === FilterType.Employee) {
                odataUrl = odataUrl +
                    `?$filter=startswith(Name, '${filter}') eq true ` +
                    `or startswith(Surname, '${filter}') eq true ` +
                    `or startswith(Patronymic, '${filter}') eq true ` +
                    `or startswith(Organization, '${filter}') eq true ` +
                    `or startswith(Position, '${filter}') eq true`;
            }
            else {
                odataUrl = odataUrl + `?$filter=Contacts/any(c: startswith(c/Value, '${filter}') eq true)`;
            }
        }
        if (appState && appState.employees) {
            fetch(odataUrl)
                .then(response => response.json())
                .then(data => {
                    dispatch({ type: 'RECEIVE_EMPLOYEES', employees: data.result });
                });
        }
    },
    openNewModal: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: "OPEN_EDIT_MODAL", employee: EmployeeReducer.unloadedState });
    },
    openEditModal: (employee: EmployeeState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: "OPEN_EDIT_MODAL", employee: employee });
    },
    closeModal: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: "CLOSE_EDIT_MODAL" });
    },
    deleteEmployee: (id: number): AppThunkAction<KnownAction> => async (dispatch, getState) =>  {
        const client: Client.Client = new Client.Client();
        try {
            const response = await client.deleteEmployee(id);
            dispatch({ type: "DELETE_EMPLOYEE", id: id });
        } catch (e) {
            const error = e.Envelope.errorMessage;
        }
    },
};

const unloadedState: EmployeesState = { employees: [], isModalOpen: false, current: EmployeeReducer.unloadedState };

export const reducer: Reducer<EmployeesState> = (state: EmployeesState | undefined, incomingAction: Action): EmployeesState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;

    switch (action.type) {
        case 'RECEIVE_EMPLOYEES':
            return {
                employees: action.employees.map(employee => Object.assign({},
                    employee,
                    {
                        contacts: employee.contacts.map(
                            (contact, index) => Object.assign({}, contact, { id: index, isValid: true })),
                        errors: []
                    })),
                isModalOpen: false,
                current: state.current,
            };
        case 'OPEN_EDIT_MODAL':
            return Object.assign({}, state, { isModalOpen: true, current: action.employee });
        case 'ADD_EMPLOYEE':
            const employeeToAdd = Object.assign({}, action.employee, {
                contacts: action.employee.contacts.map(
                    (contact, index) => Object.assign({}, contact, { id: index, isValid: true })),
            });
            return state.employees.filter(employee => employee.id === employeeToAdd.id).length === 0
                ? Object.assign({}, state, { employees: [...state.employees, employeeToAdd] })
                : Object.assign({},
                    state,
                    {
                        employees: state.employees.map(employee => {
                            return employee.id === action.employee.id ? employeeToAdd : employee;
                        })
                    });
        case 'CLOSE_EDIT_MODAL':
            return Object.assign({}, state, { isModalOpen: false, errors: [] });
        case 'DELETE_EMPLOYEE':
            return Object.assign({}, state, { employees: state.employees.filter(employee => employee.id !== action.id) });

        case 'CREATE_CONTACT':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'UPDATE_CONTACT':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'DELETE_CONTACT':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }

        case 'UPDATE_EMPLOYEE':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        default:
            return state;
    }
};
