
import React from 'react';
import {useField } from 'formik';
import { Select, InputLabel, FormControl } from '@mui/material';

export const DropDown = ({ label, ...props }) => {
    const [field, meta] = useField(props);

    const handleChange = (event) => {
        const { value } = event.target;
        field.onChange(event);
    };

    const customMenuProps = {
        PaperProps: {
            style: {
                maxHeight: 150, 
                overflow: 'auto', 
            },
        },
    };

    return (
        <FormControl fullWidth className={"dropdown"} style={ { marginTop: '2.5rem'}}>
            <InputLabel id={`${props.id}-label`}>{label}</InputLabel>
            <Select
                labelId={`${props.id}-label`}
                id={props.id}
                {...field}
                onChange={handleChange}
                value={field.value}
                label={label}
                MenuProps={customMenuProps}
            >
                {props.children}
            </Select>
        </FormControl>
    );
};