import { Action, Reducer } from 'redux';
import { KnownAction } from './EmployeeActions';
import { EmployeeState, ContactState } from './EmployeeState';

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
                            return Object.assign({}, contact, { value: action.value });
                        }
                        return contact;
                    })
                });
        case 'SET_CONTACT_TYPE':
            return Object.assign({}, state,
                {
                    contacts: state.contacts.map(contact => {
                        if (contact.id === action.id) {
                            return Object.assign({}, contact, { type: action.contactType });
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
