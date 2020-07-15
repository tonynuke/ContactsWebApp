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
    errors: [],
    contacts: []
};

function isContactValid(contact: ContactState): string {
    if (contact.type === ContactType.Email) {
        const regexp: RegExp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
        const isValidEmail: boolean = regexp.test(contact.value);
        if (isValidEmail === false)
            return `${contact.value} is not valid Email`;
    }
    if (contact.value.length === 0)
        return `value can't be empty`;

    return "";
}

export const reducer: Reducer<EmployeeState> = (state: EmployeeState | undefined, incomingAction: Action): EmployeeState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'UPDATE_EMPLOYEE':
            {
                let errors: string[] = [];
                if (action.employee.name.length === 0) {
                    errors.push("Name can't be empty");
                }
                const contactsErrors: string[] = action.employee.contacts
                    .map(contact => isContactValid(contact))
                    .filter(validationResult => validationResult.length > 0);

                return Object.assign({}, action.employee, { errors: errors.concat(contactsErrors) });
            }
        case 'CREATE_CONTACT':
            {
                const nextId: number = state.tmpContactId - 1;
                const newContact: ContactState = { id: nextId, type: action.newType, value: action.newValue, isValid: false };
                const validationResult = isContactValid(newContact);
                const errors = validationResult.length === 0 ? [] : [validationResult];
                return Object.assign({}, state, {
                    contacts: [...state.contacts, newContact], tmpContactId: nextId,
                    errors: errors
                });
            }
        case 'UPDATE_CONTACT':
            {
                const validationResult = isContactValid(action.contact);
                const isValid = validationResult.length === 0;
                const errors: string[] = isValid ? [] : [validationResult];
                return Object.assign({},
                    state,
                    {
                        contacts: state.contacts.map(contact => {
                            if (contact.id === action.contact.id) {

                                return Object.assign({}, action.contact, { isValid: isValid });
                            }
                            return contact;
                        }),
                        errors: errors
                    });
            }
        case 'DELETE_CONTACT':
            return Object.assign({}, state, { contacts: state.contacts.filter(contact => contact.id !== action.id) });
        default:
            return state;
    }
};
