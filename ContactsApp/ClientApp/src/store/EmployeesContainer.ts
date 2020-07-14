import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import buildQuery from 'odata-query'
import * as Employee from "./EmployeeActions";
import { EmployeeState, ContactState } from "./EmployeeState";
import * as EmployeeReducer from "./EmployeeReducer";

interface OdataEmployeesResponse {
    value: EmployeeState[];
}

export interface EmployeesState {
    employees: EmployeeState[];
    isModalOpen: boolean;

    current: EmployeeState;
}

export interface OpenNewModalAction {
    type: 'OPEN_NEW_MODAL';
}

export interface OpenEditModalAction {
    type: 'OPEN_EDIT_MODAL';
    employee: EmployeeState;
}

export interface CloseModalAction {
    type: 'CLOSE_MODAL';
}

export interface DeleteEmployeeAction {
    type: 'DELETE_EMPLOYEE';
    id: number;
}

interface ReceiveEmployeesAction {
    type: 'RECEIVE_EMPLOYEES';
    employees: EmployeeState[];
}

export type KnownAction = ReceiveEmployeesAction | OpenNewModalAction | OpenEditModalAction | CloseModalAction | DeleteEmployeeAction
    | Employee.KnownAction;

export const actionCreators = {
    saveEmployee: (employee: EmployeeState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        let requestType: string = employee.id === -1 ? 'POST' : 'PUT';
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
                if (response.status === 200) {
                    const appState = getState();
                    const expand = 'Contacts';
                    const query = buildQuery({ expand });
                    if (appState && appState.employees) {
                        fetch(`odata/employees${query}`)
                            .then(response => response.json() as Promise<OdataEmployeesResponse>)
                            .then(data => {
                                dispatch({ type: 'RECEIVE_EMPLOYEES', employees: data.value });
                            }).then(data => {
                                dispatch({ type: 'CLOSE_MODAL' });
                            });
                    }
                }
            });
    },
    requestEmployees: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        const expand = 'Contacts';
        const query = buildQuery({ expand });
        if (appState && appState.employees) {
            fetch(`odata/employees${query}`)
                .then(response => response.json() as Promise<OdataEmployeesResponse>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_EMPLOYEES', employees: data.value });
                });
        }
    },
    openNewModal: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'OPEN_NEW_MODAL' });
    },
    openEditModal: (employee: EmployeeState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'OPEN_EDIT_MODAL', employee: employee });
    },
    closeModal: (needSave: boolean): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CLOSE_MODAL' });
    },
    deleteEmployee: (id: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const request: RequestInit = {
            method: 'DELETE',
            body: JSON.stringify({ ids: [id] }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        };
        fetch('employees', request)
            .then(response => response)
            .then(data => {
                dispatch({ type: 'DELETE_EMPLOYEE', id: id });
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
                    contacts: employee.contacts.map(
                        (contact, index) => Object.assign({}, contact, { id: index, isValid: true }))
                })),
                isModalOpen: false,
                current: state.current
            };
        case 'OPEN_NEW_MODAL':
        {
            const newEmployee: EmployeeState =
                Object.assign({}, {} as EmployeeState, { id: -1, contacts: [], tmpContactId: -1, birthDate: new Date() });
                return Object.assign({}, state, { isModalOpen: true, current: newEmployee });
            }
        case 'OPEN_EDIT_MODAL':
            return Object.assign({}, state, { isModalOpen: true, current: action.employee });
        case 'CLOSE_MODAL':
            return Object.assign({}, state, { isModalOpen: false });
        case 'DELETE_EMPLOYEE':
            {
                const notFoundIndex = -1;
                const deletedIds = [action.id];
                return Object.assign({}, state, { employees: state.employees.filter(employee => deletedIds.indexOf(employee.id) === notFoundIndex) });
            }
        case 'SET_EMPLOYEE_NAME':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SET_EMPLOYEE_SURNAME':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SET_EMPLOYEE_PATRONYMIC':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SET_EMPLOYEE_BIRTHDATE':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SET_EMPLOYEE_ORGANIZATION':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SET_EMPLOYEE_POSITION':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }

        case 'CREATE_CONTACT':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SET_CONTACT_VALUE':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SET_CONTACT_TYPE':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'DELETE_CONTACT':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SAVE_EMPLOYEE':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        default:
            return state;
    }
};
