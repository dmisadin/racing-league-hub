import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export const slugValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const value: string = control.value;

    if (!value) return null;

    if (!/^[a-z0-9]/.test(value))
        return { slug: 'Must start with a lowercase letter or digit' };

    if (!/[a-z0-9]$/.test(value))
        return { slug: 'Must end with a lowercase letter or digit' };

    if (/[^a-z0-9-]/.test(value))
        return { slug: 'Only lowercase letters, digits, and hyphens allowed' };

    if (/-{2,}/.test(value))
        return { slug: 'Consecutive hyphens are not allowed' };

    return null;
};