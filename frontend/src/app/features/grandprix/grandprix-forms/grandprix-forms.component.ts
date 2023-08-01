import { Component } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { GrandprixInfoItemComponent } from './grandprix-info-item/grandprix-info-item.component';
import { GrandPrixInsert } from 'app/shared/models/grandprix/GrandPrixInsert';
import { firstValueFrom } from 'rxjs';
import { AddGrandprixService } from 'app/core/services/add-grandprix.service';


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

	constructor(private fb: FormBuilder, private addGrandPrixService: AddGrandprixService) { }

	onSubmit(): void {
		this.isSubmitted = true;
		console.log("Submitted form: ", this.grandPrixForm.value);
		//Add redirect from form to Season page.
    var value = this.getFormArray('list').value;

    var grandPrixInsert : Array<GrandPrixInsert> = new Array<GrandPrixInsert>();
    value.forEach((item: GrandPrixInsert) => {
      grandPrixInsert.push(item);
    });

    grandPrixInsert.forEach((item: GrandPrixInsert) => {
      item.seasonId = 1;
    })

    console.log(grandPrixInsert);
    this.addGrandPrix(grandPrixInsert);
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

  async addGrandPrix(grandPrixInserts: Array<GrandPrixInsert>): Promise<void>{
    const result = await firstValueFrom(this.addGrandPrixService.addGrandPrix(grandPrixInserts));
    console.log(result);
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
