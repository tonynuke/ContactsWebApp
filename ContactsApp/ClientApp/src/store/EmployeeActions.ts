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
    birthDate: Date;
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

export interface SaveEmployeeAction {
    type: 'SAVE_EMPLOYEE';
    employee: EmployeeState;
}

export interface SetLinkValueAction {
    type: 'SET_LINK_VALUE';
    id: number;
    value: string;
}

export interface SetLinkTypeAction {
    type: 'SET_LINK_TYPE';
    id: number;
    linkType: string;
}

export type KnownAction = SetEmployeeNameAction
    | SetEmployeeSurnameAction
    | SetEmployeePatronymicAction
    | SetEmployeeBirthDateAction
    | SetEmployeeOrganizationAction
    | SetEmployeePositionAction
    | CreateLinkAction
    | DeleteLinkAction
    | SetLinkValueAction
    | SetLinkTypeAction
    | SaveEmployeeAction;

export const actionCreators = {
    createLink: (newValue: string, newType: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CREATE_LINK', newValue: newValue, newType: newType });
    },
    deleteLink: (link: LinkState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'DELETE_LINK', link: link });
    },
    setName: (name: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_EMPLOYEE_NAME', name: name });
    },
    setSurname: (surname: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_EMPLOYEE_SURNAME', surname: surname });
    },
    setPatronymic: (patronymic: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_EMPLOYEE_PATRONYMIC', patronymic: patronymic });
    },
    setBirthDate: (birthDate: Date): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_EMPLOYEE_BIRTHDATE', birthDate: birthDate });
    },
    setOrganization: (organization: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_EMPLOYEE_ORGANIZATION', organization: organization });
    },
    setPosition: (position: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_EMPLOYEE_POSITION', position: position });
    },
    setLinkValue: (id: number, value: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_LINK_VALUE', id: id, value: value });
    },
    setLinkType: (id: number, linkType: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_LINK_TYPE', id: id, linkType: linkType });
    },
};