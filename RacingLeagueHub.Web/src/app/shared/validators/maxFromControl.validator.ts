import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function maxFromControl(controlName: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if (!control.parent) return null;

        const max = control.parent.get(controlName)?.value;
        const value = control.value;

        if (max == null || value == null) return null;

        return value > max ? { max: { max, actual: value } } : null;
    };
}