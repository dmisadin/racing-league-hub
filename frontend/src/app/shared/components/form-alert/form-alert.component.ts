import { Component, Input, SimpleChanges } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors } from '@angular/forms';

interface AllValidationErrors {
	control: string;
	error: string;
	value: any;
	text?: string;
}

export interface FormGroupControls {
	[key: string]: AbstractControl;
}

@Component({
	selector: 'app-form-alert',
	templateUrl: './form-alert.component.html',
	styleUrls: ['./form-alert.component.scss']
})
export class FormAlertComponent {
	@Input() form!: FormGroup;
	@Input() singleControl: AbstractControl | null = null;
	errorArray: AllValidationErrors[] = [];


	ngOnChanges(changes: SimpleChanges) {
		if (changes['form'] && this.form) {
			// The reference to the form group changed, manually handle changes here
			this.form.valueChanges.subscribe(() => {
				// Handle changes within the form group
				this.errorArray = this.validateAllFormControl(this.form);
				console.log(this.errorArray);
				if (this.form.invalid)
					this.errorArray.forEach(error => this.addErrorText(error));
			});
		}
	}

	validateAllFormControl(formGroup: FormGroup): AllValidationErrors[] {
		let result: AllValidationErrors[] = [];
		Object.keys(formGroup.controls).forEach(field => {
			const control = formGroup.get(field);
			if (control instanceof FormControl) {
				this.validateSingleFormControl(control, field, result);
			} else if (control instanceof FormGroup) {
				this.validateAllFormControl(control);
			}
		});
		return result;
	}

	validateSingleFormControl(control: FormControl, field: string, result: AllValidationErrors[]) {
		const controlErrors: ValidationErrors | null = control.errors;
		if (controlErrors) {
			Object.keys(controlErrors).forEach(keyError => {
				result.push({
					'control': field,
					'error': keyError,
					'value': controlErrors[keyError]
				});
			});
		}
	}
	addErrorText(error: AllValidationErrors) {
		if (error.error === "required") error.text = `${error.control} is a required field.`
		else if (error.error === "min") error.text = `${error.control} has to be larger than ${error.value.min}.`
		else if (error.error === "max") error.text = `${error.control} has to be smaller than ${error.value.max}.`
	}
}


/*
				const controlErrors: ValidationErrors | null = control.errors;
				if (controlErrors) {
					Object.keys(controlErrors).forEach(keyError => {
						result.push({
							'control': field,
							'error': keyError,
							'value': controlErrors[keyError]
						});
					});
				}
*/