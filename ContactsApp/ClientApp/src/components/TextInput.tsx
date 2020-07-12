import React, { FunctionComponent } from 'react';

export type TextInputProps = {
    handleChange: Function,
    title: string,
    value: string,
}

export const TextInput: FunctionComponent<TextInputProps> = ({ handleChange, title, value }) =>
    <div>
        {title}
        <br />
        <input type="text" value={value}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => handleChange(event.target.value)} />
        <br />
    </div>