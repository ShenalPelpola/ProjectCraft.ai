import PropTypes from 'prop-types';
import { useState } from 'react';
import { Empties } from '../../../Constants/Empties';
import SubmitButton from '../../../assets/send.svg';
import './PromptTextBox.scss';

export const PromptTextBox = ({
    initialValue,
    placeholder,
    className,
    onSearchSubmit
}) => {
    const [searchTerm, setSearchTerm] = useState(initialValue || Empties.emptyString);

    const handleSubmit = (event) => {
        event.preventDefault();
        onSearchSubmit(searchTerm);
    };

    return (
        <form onSubmit={handleSubmit} className={className || 'prompt-textbox'}>
            <input
                value={searchTerm}
                placeholder={placeholder}
                onChange={(e) => setSearchTerm(e.target.value)}
            />
            <button type="submit" className="btn-prompt-submit">
                <img src={SubmitButton} alt="submit-button" />
            </button>
        </form>
    )
};

PromptTextBox.propTypes = {
    /* Initial value to display as the prompt*/
    initialValue: PropTypes.string,
    /* Placeholder to dislay in the input */
    placeholder: PropTypes.string,
    /* Function to execute when submitting */
    onSearchSubmit: PropTypes.string
};

PromptTextBox.defaultProps = {
    initialValue: null,
    placeholder: Empties.emptyString,
    onSearchSubmit: Empties.noop
};