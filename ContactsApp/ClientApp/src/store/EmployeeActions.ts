import { AppThunkAction } from './';
import { LinkState, EmployeeState } from "./EmployeeState";

export interface SetEmployeeNameAction {
    type: 'SET_EMPLOYEE_NAME';
    name: string;
}

export interface SetEmployeeSurnameAction {
    type: 'SET_EMPLOYEE_SURNAME';
    surname: string;
}

export interface SetEmployeePatronymicAction {
    type: 'SET_EMPLOYEE_PATRONYMIC';
    patronymic: string;
}

export interface SetEmployeeBirthDateAction {
    type: 'SET_EMPLOYEE_BIRTHDATE';
    date: Date;
}

export interface SetEmployeeOrganizationAction {
    type: 'SET_EMPLOYEE_ORGANIZATION';
    organization: string;
}

export interface SetEmployeePositionAction {
    type: 'SET_EMPLOYEE_POSITION';
    position: string;
}

export interface CreateLinkAction {
    type: 'CREATE_LINK';
    newValue: string;
    newType: string;
}

export interface DeleteLinkAction {
    type: 'DELETE_LINK';
    link: LinkState;
}

export interface UpdateLinkAction {
    type: 'UPDATE_LINK';
    link: LinkState;
    newValue: string;
    newType: string;
}

export interface SaveEmployeeAction {
    type: 'SAVE_EMPLOYEE';
    employee: EmployeeState;
}

export interface UpdateEmployeeNameAction {
    type: 'UPDATE_EMPLOYEE_NAME';
    name: string;
}

export interface UpdateEmployeeSurnameAction {
    type: 'UPDATE_EMPLOYEE_SURNAME';
    surname: string;
}

export interface UpdateEmployeePositionAction {
    type: 'UPDATE_EMPLOYEE_POSITION';
    position: string;
}

export type KnownAction = SetEmployeeNameAction | SetEmployeeSurnameAction | SetEmployeePatronymicAction | SetEmployeeBirthDateAction
    | SetEmployeeOrganizationAction | SetEmployeePositionAction
    | CreateLinkAction | UpdateLinkAction | DeleteLinkAction
    | SaveEmployeeAction;

export const actionCreators = {
    saveEmployee: (employee: EmployeeState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const request: RequestInit = {
            method: 'POST',
            body: JSON.stringify(employee),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        };
        fetch('employees', request)
            .then(response => response.json() as Promise<number>)
            .then(data => {
                dispatch({ type: 'SAVE_EMPLOYEE', employee: employee });
            });
    },
    createLink: (newValue: string, newType: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CREATE_LINK', newValue: newValue, newType: newType });
    },
    deleteLink: (link: LinkState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'DELETE_LINK', link: link });
    },
    updateLink: (newValue: string, newType: string, link: LinkState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'UPDATE_LINK', newValue: newValue, newType: newType, link: link });
    },
    setName: (name: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_EMPLOYEE_NAME', name: name });
    },
    setPosition: (position: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_EMPLOYEE_POSITION', position: position });
    },
};