import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import buildQuery from 'odata-query'
import * as Employee from "./EmployeeActions";
import { EmployeeState, LinkState, State } from "./EmployeeState";
import * as EmployeeReducer from "./EmployeeReducer";

interface OdataEmployeesState {
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
    needSave: boolean;
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
    requestEmployees: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        const expand = 'Links';
        const query = buildQuery({ expand });
        if (appState && appState.employees) {
            fetch(`odata/employees${query}`)
                .then(response => response.json() as Promise<OdataEmployeesState>)
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
        dispatch({ type: 'CLOSE_MODAL', needSave: needSave });
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

const unloadedState: EmployeesState = { employees: [], isModalOpen: false, current: {} as EmployeeState };

export const reducer: Reducer<EmployeesState> = (state: EmployeesState | undefined, incomingAction: Action): EmployeesState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;

    switch (action.type) {
        case 'DELETE_EMPLOYEE':
            {
                const notFoundIndex = -1;
                const deletedIds = [action.id];
                return {
                    employees: state.employees.filter(employee => deletedIds.indexOf(employee.id) === notFoundIndex),
                    isModalOpen: false,
                    current: state.current
                };
            }
        case 'RECEIVE_EMPLOYEES':
            return {
                employees: action.employees, isModalOpen: false,
                current: state.current
            };
        case 'OPEN_NEW_MODAL':
            {
                const newEmployee: EmployeeState = {
                    id: -1, name: '', surname: '', patronymic: '', organization: '', position: '', links: [], state: State.New
                };
                return {
                    employees: state.employees,
                    isModalOpen: true,
                    current: newEmployee
                };
            }
        case 'OPEN_EDIT_MODAL':
            return {
                employees: state.employees,
                isModalOpen: true,
                current: action.employee
            }
        case 'CLOSE_MODAL':
            return {
                employees: state.employees,
                isModalOpen: false,
                current: state.current
            }
        case 'SET_EMPLOYEE_NAME':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SET_EMPLOYEE_POSITION':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'CREATE_LINK':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'UPDATE_LINK':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'DELETE_LINK':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        case 'SAVE_EMPLOYEE':
            return { ...state, current: EmployeeReducer.reducer(state.current, action) }
        default:
            return state;
    }
};
