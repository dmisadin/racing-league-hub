import { Component } from '@angular/core';
import { ControlContainer, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
	selector: 'app-season-lobby-settings',
	templateUrl: './season-lobby-settings.component.html',
	styleUrls: ['./season-lobby-settings.component.scss']
})
export class SeasonLobbySettingsComponent {
	constructor(private fb: FormBuilder, private parentControl: ControlContainer) { }

	parentFormGroup = this.parentControl.control as FormGroup;
	switchState = false;

	//Check if this can be done dynamicly? Get list of attributes from DB and generate form.
	// Note: very difficult considering they store different data types (boolean use checkmark)
	// Idea: group them by data type and use ngFor to render them?
	lobbySettingsForm = this.fb.group({
		qualifying: ["Short", Validators.required],
		formationLap: [true, Validators.required],
		raceDistancePercentage: [50, [Validators.required, Validators.min(25), Validators.max(100)]],
		weather: ["Dynamic", Validators.required],
		cornerCutting: ["Strict", Validators.required],
		carDamage: ["Reduced", Validators.required],
		carDamageRate: ["Standard", Validators.required],
		parcFerme: [true, Validators.required],
		equalCarPerformance: [true, Validators.required],
		safetyCar: ["Reduced", Validators.required],
		collisions: [true, Validators.required],
		ghosting: [false, Validators.required],
		start: ["Manual", Validators.required],
	})

	ngOnInit(): void {
		this.parentFormGroup.addControl('lobbySettings', this.lobbySettingsForm);
		this.lobbySettingsForm.get('test')?.value
	}

	toOnOff(statement: boolean) {
		return statement ? "On" : "Off";
	}

	mockLobby = [
		{ iconPath: 'lobby/qualifying.png', label: 'Qualifying', value: 'Short' },
		{ iconPath: 'lobby/formationlap.png', label: 'Formation Lap', value: true },
		{ iconPath: 'lobby/racedistancepercentage.png', label: 'Race Distance', value: 50 },
		{ iconPath: 'lobby/weather.png', label: 'Weather', value: 'Dynamic' },
		{ iconPath: 'lobby/cornercutting.png', label: 'Corner cutting', value: 'strict' },
		{ iconPath: 'lobby/damage.png', label: 'Damage', value: 'Reduced' },
		{ iconPath: 'lobby/damage.png', label: 'Damage Rate', value: 'Standard' },
		{ iconPath: 'lobby/parcferme.png', label: 'Parc Ferme', value: true },
		{ iconPath: 'lobby/equalcarperformance.png', label: 'Engine', value: true },
		{ iconPath: 'lobby/safetycar.png', label: 'Safety Car', value: 'Reduced' },
		{ iconPath: 'lobby/collisions.png', label: 'Collisions', value: true },
		{ iconPath: 'lobby/ghosting.png', label: 'Ghosting', value: false },
		{ iconPath: 'lobby/start.png', label: 'Starts', value: 'Manual' },
	]
}
