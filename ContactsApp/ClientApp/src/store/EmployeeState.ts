export enum ContactType {
    Skype = "Skype",
    Email = "Email",
}

export interface ContactState {
    id: number;
    value: string;
    type: ContactType;

    isValid: boolean;
}

export interface EmployeeState {
    id: number;
    name: string;
    surname: string;
    birthDate: Date;
    patronymic: string;
    position: string;
    organization: string;

    tmpContactId: number;

    contacts: ContactState[];
}