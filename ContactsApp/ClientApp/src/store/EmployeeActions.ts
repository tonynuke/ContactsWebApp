import { AppThunkAction } from './';
import { EmployeeState, ContactType } from "./EmployeeState";

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

export interface CreateContactAction {
    type: 'CREATE_CONTACT';
    newValue: string;
    newType: ContactType;
}

export interface DeleteContactAction {
    type: 'DELETE_CONTACT';
    id: number;
}

export interface SaveEmployeeAction {
    type: 'SAVE_EMPLOYEE';
    employee: EmployeeState;
}

export interface SetContactValueAction {
    type: 'SET_CONTACT_VALUE';
    id: number;
    value: string;
}

export interface SetContactTypeAction {
    type: 'SET_CONTACT_TYPE';
    id: number;
    contactType: ContactType;
}

export type KnownAction = SetEmployeeNameAction
                          | SetEmployeeSurnameAction
                          | SetEmployeePatronymicAction
                          | SetEmployeeBirthDateAction
                          | SetEmployeeOrganizationAction
                          | SetEmployeePositionAction
                          | CreateContactAction
                          | DeleteContactAction
                          | SetContactValueAction
                          | SetContactTypeAction
                          | SaveEmployeeAction;

export const actionCreators = {
    createContact: (newValue: string, newType: ContactType): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CREATE_CONTACT', newValue: newValue, newType: newType });
    },
    deleteContact: (id: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'DELETE_CONTACT', id: id });
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
    setContactValue: (id: number, value: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_CONTACT_VALUE', id: id, value: value });
    },
    setContactType: (id: number, contactType: ContactType): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'SET_CONTACT_TYPE', id: id, contactType: contactType });
    },
};