import React from 'react';
import { Button } from '../../CustomUis/Button/Button';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import TextField from '@mui/material/TextField';
import Checkbox from '@mui/material/Checkbox';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import Autocomplete from '@mui/material/Autocomplete';
import { DropDown } from '../../CustomUis/Dropdown/DropDown';
import { MenuItem } from '@mui/material';
import * as Yup from 'yup';
import './ProjectCreateWindow.scss';

export const ProjectCreateWindow = ({ isOpen, onClose }) => {
  const validationSchema = Yup.object({
    solutionName: Yup.string().required('Solution name is required'),
    SDKVersion: Yup.string().required('SDK version is required')
  });

  const handleSubmission = (values, { setSubmitting }) => {
    console.log('Form Data:', values);
    setSubmitting(false);
  };

  const CustomTextInput = ({ field, form: { touched, errors }, ...props }) => {
    return (
      <div>
        <TextField label="Solution name" variant="standard" {...field} {...props} />
        {touched[field.name] && errors[field.name] ? (
          <div className="error-message">{errors[field.name]}</div>
        ) : null}
      </div>
    );
  };

  const CustomCheckbox = ({ field, ...props }) => {
    return (
      <div className="standalone-project">
        <Checkbox {...field} {...props} />
        {props.label}
      </div>
    );
  };

  const VersionDropdown = () => {
    return (
      <Autocomplete
        disablePortal
        id="versions"
        options={[{ value: 1, label: '8.0.0' }]}
        sx={{ width: 300 }}
        renderInput={(params) => <TextField {...params} label="version" value="value" />}
      />
    );
  }

  return (
    <Dialog open={isOpen} onClose={onClose} fullWidth={true}>
      <DialogTitle sx={{ m: 0, p: 2 }}>
        Create Project
      </DialogTitle>
      <DialogContent dividers>
        <Formik
          initialValues={{ solutionName: '', placeInProjectDirectory: false, SDKVersion: '' }}
          validationSchema={validationSchema}
          onSubmit={handleSubmission}
        >
          {formik => (
            <Form>
              <Field
                className="solution-field"
                text="Solution"
                component={CustomTextInput}
                name="solutionName"
                id="solutionName"
              />
              <DropDown
                name="SDKVersion"
                label=".NET SDK version"
                id="sdkVersion"
              >
                <MenuItem value=""><em>None</em></MenuItem>
                <MenuItem value="8.0.0">8.0.0</MenuItem>
              </DropDown>
              <Field
                type="checkbox"
                className="standalone-project"
                component={CustomCheckbox}
                name="placeInProjectDirectory"
                label={"Place solution in project directory"}
                id="placeInProjectDirectory"
              />
              <div className="btn-container">
                <Button className="create-project-close-btn" onClick={onClose}>
                  Close
                </Button>
                <Button type="submit" className="create-project-submit-btn" disabled={formik.isSubmitting} autoFocus>
                  Save changes
                </Button>
              </div>
            </Form>
          )}
        </Formik>
      </DialogContent>
    </Dialog>
  );
};