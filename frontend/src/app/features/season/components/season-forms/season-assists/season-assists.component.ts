import { Component } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup } from '@angular/forms';


@Component({
	selector: 'app-season-assists',
	templateUrl: './season-assists.component.html',
	styleUrls: ['./season-assists.component.scss']
})
export class SeasonAssistsComponent {

	constructor(private fb: FormBuilder, private parentControl: ControlContainer) { }

	parentFormGroup = this.parentControl.control as FormGroup;

	assistsForm = this.fb.group({
		tractionControl: "Off",
		abs: false,
		racingLine: "Corners only",
		gearbox: "Manual"
	})

	ngOnInit(): void {
		this.parentFormGroup.addControl('assists', this.assistsForm);
	}

	toOnOff(statement: boolean) {
		return statement ? "On" : "Off";
	}

	mockAssists = [
		{ iconPath: 'assists/tractioncontrol.png', label: 'Traction Control', value: 'Medium' },
		{ iconPath: 'assists/abs.png', label: 'ABS', value: 'Off' },
		{ iconPath: 'assists/racingline.png', label: 'Racing Line', value: 'Corners only' },
		{ iconPath: 'assists/gearbox.png', label: 'gearbox', value: 'Manual' },
	];
}
