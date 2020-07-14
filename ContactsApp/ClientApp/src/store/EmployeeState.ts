export interface ContactState {
    id: number;
    value: string;
    type: string;

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