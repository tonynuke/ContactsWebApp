import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface OdataEmployeesState {
    value: EmployeeState[];
}

export interface EmployeesState {
    employees: EmployeeState[];
    isModalOpen: boolean;

    current: EmployeeState;
}

export interface EmployeeState {
    id: number;
    name: string;
    surname: string;
    patronymic: string;
    position: string;
    organization: string;

    links: LinkState[];
}

export interface LinkState {
    id: number;
    value: string;
    type: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

export interface OpenCreateModalAction {
    type: 'OPEN_CREATE_MODAL';
}

export interface OpenEditModalAction {
    type: 'OPEN_EDIT_MODAL';
    employee: EmployeeState;
}

export interface CloseModalAction {
    type: 'CLOSE_MODAL';
}

export interface CreateEmployeeAction {
    type: 'CREATE_EMPLOYEE';
    employee: EmployeeState;
}

export interface DeleteEmployeeAction {
    type: 'DELETE_EMPLOYEE';
    ids: number[];
}

interface ReceiveEmployeesAction {
    type: 'RECEIVE_EMPLOYEES';
    employees: EmployeeState[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
export type KnownAction = CreateEmployeeAction | DeleteEmployeeAction | ReceiveEmployeesAction
    | OpenCreateModalAction | OpenEditModalAction | CloseModalAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestEmployees: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.employees) {
            fetch(`odata/employees`)
                .then(response => response.json() as Promise<OdataEmployeesState>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_EMPLOYEES', employees: data.value });
                });
        }
    },
    createEmployee: (name: string, position: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        const newEmployee = { name: name, position: position };
        const request: RequestInit = {
            method: 'POST',
            body: JSON.stringify(newEmployee),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        };
        if (appState && appState.employees) {
            fetch(`employees`, request)
                .then(response => response.json() as Promise<number>)
                .then(data => {
                    const employee: EmployeeState = { id: data, name: name, position: position, patronymic: '', surname: '', organization: '', links: [] };
                    dispatch({ type: 'CREATE_EMPLOYEE', employee: employee });
                });
        }
    },
    deleteEmployee: (ids: number[]): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        const request: RequestInit = {
            method: 'DELETE',
            body: JSON.stringify({ ids: ids }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        };
        if (appState && appState.employees) {
            fetch(`employees`, request)
                .then(response => response)
                .then(data => {
                    dispatch({ type: 'DELETE_EMPLOYEE', ids: ids });
                });
        }
    },
    openCreateModal: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'OPEN_CREATE_MODAL' });
    },
    openEditModal: (employee: EmployeeState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'OPEN_EDIT_MODAL', employee: employee });
    },
    closeModal: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CLOSE_MODAL' });
    },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: EmployeesState = { employees: [], isModalOpen: false, current: {} as EmployeeState };

export const reducer: Reducer<EmployeesState> = (state: EmployeesState | undefined, incomingAction: Action): EmployeesState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'CREATE_EMPLOYEE':
            return {
                employees: [...state.employees, action.employee],
                isModalOpen: false,
                current: state.current
            };
        case 'DELETE_EMPLOYEE':
            {
                const notFoundIndex = -1;
                const deletedIds = action.ids;
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
        case 'OPEN_CREATE_MODAL':
            return {
                employees: state.employees,
                isModalOpen: true,
                current: {} as EmployeeState
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
        default:
            return state;
    }
};
