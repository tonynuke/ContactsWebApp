import { AppThunkAction } from './';
import { EmployeeState, ContactType, ContactState } from "./EmployeeState";

export interface UpdateEmployeeAction {
    type: 'UPDATE_EMPLOYEE';
    employee: EmployeeState;
}

export interface SaveEmployeeAction {
    type: 'SAVE_EMPLOYEE';
    employee: EmployeeState;
}

export interface CreateContactAction {
    type: 'CREATE_CONTACT';
    newId: number;
    newValue: string;
    newType: ContactType;
}

export interface UpdateContactAction {
    type: 'UPDATE_CONTACT';
    contact: ContactState;
}

export interface DeleteContactAction {
    type: 'DELETE_CONTACT';
    id: number;
}

export type KnownAction = UpdateEmployeeAction | SaveEmployeeAction
    | CreateContactAction | UpdateContactAction | DeleteContactAction;

export const actionCreators = {

    updateEmployee: (employee: EmployeeState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'UPDATE_EMPLOYEE', employee: employee });
    },
    createContact: (newId: number, newValue: string, newType: ContactType): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'CREATE_CONTACT', newId: newId, newValue: newValue, newType: newType });
    },
    updateContact: (contact: ContactState): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'UPDATE_CONTACT', contact: contact });
    },
    deleteContact: (id: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'DELETE_CONTACT', id: id });
    },
};