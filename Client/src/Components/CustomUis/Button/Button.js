import React, { forwardRef } from 'react';
import './Button.scss';

export const Button = forwardRef(({ onClick, onMouseOver, onMouseLeave, disabled, className, ...customStyles }, ref) => {
  return (
    <button
      ref={ref}
      onClick={onClick}
      disabled={disabled}
      className={`btn ${className} ${disabled && 'btn-disabled'}`}
      onMouseOver={onMouseOver}
      onMouseLeave={onMouseLeave}
      {...customStyles}
    />
  );
});