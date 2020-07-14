import { Action, Reducer } from 'redux';
import { KnownAction } from './EmployeeActions';
import { EmployeeState, ContactState, ContactType } from './EmployeeState';

export const unloadedState: EmployeeState = {
    id: -1,
    name: '',
    surname: '',
    patronymic: '',
    position: '',
    organization: '',
    birthDate: new Date(),
    tmpContactId: -1,
    contacts: []
};

function isContactValid(value: string, type: ContactType): boolean {
    switch (type) {
        case ContactType.Email:
            {
                const regexp: RegExp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
                return regexp.test(value);
            }
        case ContactType.Skype:
            {
                return value.length > 3;
            }
        default:
            return true;
    }
}

export const reducer: Reducer<EmployeeState> = (state: EmployeeState | undefined, incomingAction: Action): EmployeeState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_EMPLOYEE_NAME':
            return Object.assign({}, state, { name: action.name });
        case 'SET_EMPLOYEE_SURNAME':
            return Object.assign({}, state, { surname: action.surname });
        case 'SET_EMPLOYEE_PATRONYMIC':
            return Object.assign({}, state, { patronymic: action.patronymic });
        case 'SET_EMPLOYEE_BIRTHDATE':
            return Object.assign({}, state, { birthDate: action.birthDate });
        case 'SET_EMPLOYEE_ORGANIZATION':
            return Object.assign({}, state, { organization: action.organization });
        case 'SET_EMPLOYEE_POSITION':
            return Object.assign({}, state, { position: action.position });
        case 'CREATE_CONTACT':
            {
                // используем отрицательные id для новых контактов
                const nextId: number = state.tmpContactId > 0 || state.tmpContactId === undefined ? -1 : state.tmpContactId - 1;
                const newContact: ContactState = { id: nextId, type: action.newType, value: action.newValue, isValid: false };

                return Object.assign({}, state, { contacts: [...state.contacts, newContact], tmpContactId: nextId });
            }
        case 'SET_CONTACT_VALUE':
            return Object.assign({}, state,
                {
                    contacts: state.contacts.map(contact => {
                        if (contact.id === action.id) {

                            const isValid = isContactValid(action.value, contact.type);
                            return Object.assign({}, contact, { value: action.value, isValid: isValid });
                        }
                        return contact;
                    })
                });
        case 'SET_CONTACT_TYPE':
            return Object.assign({}, state,
                {
                    contacts: state.contacts.map(contact => {
                        if (contact.id === action.id) {
                            const isValid = isContactValid(contact.value, action.contactType);
                            return Object.assign({}, contact, { type: action.contactType, isValid: isValid });
                        }
                        return contact;
                    })
                });
        case 'DELETE_CONTACT':
            return Object.assign({}, state, { contacts: state.contacts.filter(contact => contact.id !== action.id) });
        default:
            return state;
    }
};
