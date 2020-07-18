import { Action, Reducer } from 'redux';
import { KnownAction } from './EmployeeActions';
import { EmployeeState, ContactState, ContactType } from './EmployeeState';

export const unloadedState: EmployeeState = {
    id: -1,
    name: 'name',
    surname: '',
    patronymic: '',
    position: '',
    organization: '',
    birthDate: new Date(),
    errors: [],
    contacts: []
};

function validateContact(contact: ContactState): string {
    const emailRegexp: RegExp = new RegExp(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/);
    const phoneRegexp: RegExp = new RegExp(/^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/);
    let error: string = '';

    switch (contact.type) {
        case ContactType.Email:
            if (emailRegexp.test(contact.value) === false)
                error = `${contact.value} is not valid Email. Valid example is my@mail.com`;
            break;
        case ContactType.Phone:
            if (phoneRegexp.test(contact.value) === false)
                error = `${contact.value} is not valid Phone. Valid example 88005553535`;
            break;
        default:
            if (contact.value.length === 0)
                error = `${contact.type} value can't be empty`;
    }

    return error;
}

export const reducer: Reducer<EmployeeState> = (state: EmployeeState | undefined, incomingAction: Action): EmployeeState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'UPDATE_EMPLOYEE':
            {
                let date: Date = state.birthDate;
                let errors: string[] = [];
                if (!action.employee.name) {
                    errors.push("Name can't be empty");
                }
                if (action.employee.birthDate.toString() !== "Invalid Date" && action.employee.birthDate.toString().length === 10) {
                    date = action.employee.birthDate;
                }
                const contactsErrors: string[] = action.employee.contacts
                    .map(contact => validateContact(contact))
                    .filter(validationResult => validationResult.length > 0);

                return Object.assign({}, action.employee, { errors: errors.concat(contactsErrors), birthDate: date });
            }
        case 'CREATE_CONTACT':
            {
                const newContact: ContactState = { id: action.newId, type: action.newType, value: action.newValue, isValid: false };
                const validationResult = validateContact(newContact);
                const errors = validationResult.length === 0 ? [] : [validationResult];
                return Object.assign({}, state, {
                    contacts: [...state.contacts, newContact],
                    errors: errors
                });
            }
        case 'UPDATE_CONTACT':
            {
                const validationResult = validateContact(action.contact);
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
