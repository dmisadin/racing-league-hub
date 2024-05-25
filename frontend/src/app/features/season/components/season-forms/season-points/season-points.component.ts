import { Component, Output, EventEmitter, Input } from '@angular/core';
import { AbstractControl, ControlContainer, FormArray, FormGroup } from '@angular/forms';
import { PointsItemComponent } from '../points-item/points-item.component';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { PointsType } from 'app/shared/models/enums/Enumerations';
import { IEnumItem } from 'app/shared/models/interfaces';

type SessionName = "Qualifying" | "Sprint" | "Race" | "FastestLap";

@Component({
	selector: 'app-season-points',
	templateUrl: './season-points.component.html',
	styleUrls: ['./season-points.component.scss']
})
export class SeasonPointsComponent {
	faPlus = faPlus;
    PointsType = PointsType;
	@Input() pointsType: IEnumItem = PointsType.Race;
	@Input() pointsArray!: FormArray;
	@Input() maxAmount: number = 22;
	@Output() pointsArrayChange = new EventEmitter();

	sessionType = {
		Qualifying: "qualPoints",
		Sprint: "sprintPoints",
		Race: "racePoints",
		FastestLap: "fastestLapPoints"
	};
	constructor(private parentControl: ControlContainer) { }

	parentFormGroup = this.parentControl.control as FormGroup;


	ngOnInit(): void {
		this.parentFormGroup.addControl("points" + this.pointsType.ValueName, this.pointsArray);
	}

	public addPointsItem(): void {
		if (this.pointsArray.controls.length < this.maxAmount) {
			this.pointsArray.push(PointsItemComponent.addPointsItem(this.pointsArray.controls.length + 1, 1, this.pointsType.Value));
			console.log("child controls", this.pointsArray.controls)
		}
		else
			console.log(`Maximum is ${this.maxAmount} players`);
	}

	public removePointsItem(index: number): void {
		this.pointsArray?.removeAt(index);
	}

	toFormGroup(a: AbstractControl) {
		return a as FormGroup;
	}
}
