import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import * as Employee from "./EmployeeActions";
import { EmployeeState } from "./EmployeeState";
import * as EmployeeReducer from "./EmployeeReducer";

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

export interface AddNewEmployeeAction {
    type: 'ADD_NEW_EMPLOYEE';
    employee: EmployeeState;
}

interface ReceiveEmployeesAction {
    type: 'RECEIVE_EMPLOYEES';
    employees: EmployeeState[];
}

export type KnownAction = ReceiveEmployeesAction | OpenEditModalAction | CloseEditModalAction | DeleteEmployeeAction | AddNewEmployeeAction
    | Employee.KnownAction;

const newEmployeeId: number = -1;

export const actionCreators = {
    saveEmployee: (employee: EmployeeState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        if (employee.errors.length > 0)
            return;
        const requestType: string = employee.id === newEmployeeId ? 'POST' : 'PUT';
        const request = {
            method: requestType,
            body: JSON.stringify(employee),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        };
        fetch('employees', request)
            .then(response => {
                if (response.ok) {
                    dispatch({ type: 'ADD_NEW_EMPLOYEE', employee: employee });
                    dispatch({ type: 'CLOSE_EDIT_MODAL' });
                }
            });
    },
    requestEmployees: (query: string | undefined): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const appState = getState();
        let odataUrl = `odata/employees?$Expand=Contacts`;
        if (query) {
            odataUrl = odataUrl +
                `&$filter=startswith(Name, '${query}') eq true ` +
                `or startswith(Surname, '${query}') eq true ` +
                `or startswith(Patronymic, '${query}') eq true ` +
                `or startswith(Organization, '${query}') eq true ` +
                `or startswith(Position, '${query}') eq true`;
        }
        if (appState && appState.employees) {
            fetch(odataUrl)
                .then(response => response.json())
                .then(data => {
                    dispatch({ type: 'RECEIVE_EMPLOYEES', employees: data.value });
                });
        }
    },
    openNewModal: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'OPEN_EDIT_MODAL', employee: EmployeeReducer.unloadedState });
    },
    openEditModal: (employee: EmployeeState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'OPEN_EDIT_MODAL', employee: employee });
    },
    closeModal: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CLOSE_EDIT_MODAL' });
    },
    deleteEmployee: (id: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const request: RequestInit = {
            method: 'DELETE',
            body: JSON.stringify({ id: id }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        };
        fetch('employees', request)
            .then(response => {
                if (response.ok) {
                    dispatch({ type: 'DELETE_EMPLOYEE', id: id });
                }
            });
    },
    ...Employee.actionCreators
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
                employees: action.employees.map(employee => Object.assign({}, employee, {
                    contacts: employee.contacts.map((contact, index) => Object.assign({}, contact, { id: index, isValid: true })),
                    errors: []
                })),
                isModalOpen: false,
                current: state.current,
            };
        case 'OPEN_EDIT_MODAL':
            return Object.assign({}, state, { isModalOpen: true, current: action.employee });
        case 'ADD_NEW_EMPLOYEE':
            return action.employee.id === newEmployeeId ?
                Object.assign({}, state, { employees: [...state.employees, action.employee] }) :
                Object.assign({}, state, {
                    employees: state.employees.map(employee => {
                        return employee.id === action.employee.id ? action.employee : employee;
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
        case 'SAVE_EMPLOYEE':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        default:
            return state;
    }
};
