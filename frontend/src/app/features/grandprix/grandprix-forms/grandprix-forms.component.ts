import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GrandprixInfoItemComponent } from './grandprix-info-item/grandprix-info-item.component';


@Component({
	selector: 'app-grandprix-forms',
	templateUrl: './grandprix-forms.component.html',
	styleUrls: ['./grandprix-forms.component.scss']
})
export class GrandPrixFormsComponent {
	isSubmitted: boolean = false;
	infoValue = { name: "", startTime: "", hasSprint: false, youTubeURL: 90, trackId: 0, countryId: 0 };
	maxAmount = 25;

	grandPrixForm = this.fb.group({
		list: this.fb.array([])
	});

	constructor(private fb: FormBuilder) { }

	onSubmit(): void {
		this.isSubmitted = true;
		console.log("Submitted form: ", this.grandPrixForm.value);
		//Add redirect from form to created Season page.

	}

	ngOnInit() {
		console.log(this.getFormArray('list').controls.length)
	}
	public addGrandPrixInfoItem(): void {
		if (this.getFormArray('list').controls.length < this.maxAmount) {
			this.getFormArray('list').push(GrandprixInfoItemComponent.addGrandPrixInfoItem());
			console.log("child controls", this.grandPrixForm.controls)
		}
		else
			console.log(`Maximum is ${this.maxAmount} players`);
	}

	public removeGrandPrixInfoItem(index: number): void {
		this.getFormArray('list').removeAt(index);
	}

	toFormGroup(a: AbstractControl) {
		return a as FormGroup;
	}

	getControl(name: string): FormControl {
		return this.grandPrixForm.get(name) as FormControl;
	}

	getFormArray(name: string): FormArray {
		return this.grandPrixForm.get(name) as FormArray;
	}

	checkError(error: string, ...name: string[]) {
		return name.some(el => this.getControl(el)?.hasError(error));
	}
}
