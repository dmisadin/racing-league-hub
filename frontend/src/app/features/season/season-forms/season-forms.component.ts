import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { AddSeasonService } from 'app/core/services/add-season.service';
import { Point } from 'app/shared/models/season/Point';
import { Assists } from 'app/shared/models/season/Assists';
import { Info } from 'app/shared/models/season/Info';
import { LobbySettings } from 'app/shared/models/season/LobbySettings';
import { SeasonInsert } from 'app/shared/models/season/SeasonInsert';
import { firstValueFrom } from 'rxjs';


@Component({
	selector: 'app-season-forms',
	templateUrl: './season-forms.component.html',
	styleUrls: ['./season-forms.component.scss']
})
export class SeasonFormsComponent {
	faArrowLeft = faArrowLeft;
	faArrowRight = faArrowRight;

	currentStep = 1;
	totalSteps = 8;
	preview = '';
	isSubmitted: boolean = false;
	stepFormNames = ["info", "qualPoints", "sprintPoints", "racePoints", "fastestLapPoints", "lobbySettings", "assists"]
	infoValue = { name: "", gameId: 5, platformId: 1, lapsRequiredPercentage: 90 };

	qualPointsArray = this.fb.array([]);
	sprintPointsArray = this.fb.array([]);
	racePointsArray = this.fb.array([]);
	fastestLapPointsArray = this.fb.array([]);

	seasonForm = this.fb.group({
		// The rest of form group is added inside child components with parent.addControl()
	});

	constructor(private fb: FormBuilder, private addSeasonService: AddSeasonService) { }

	onSubmit(): void {
		this.isSubmitted = true;
		console.log("Submitted form: ", this.seasonForm.value, this.seasonForm.valid, this.seasonForm);
    var info : Info = this.seasonForm.get('info')!.value;
    var fastestLapPoints : Point[] = this.seasonForm.get('fastestLapPoints')!.value;


    var seasonInsert : SeasonInsert;
    seasonInsert = {
      leagueId : 1,
      gameId : this.infoValue.gameId,
      platformId : this.infoValue.platformId,
      imagePath : '',
      name : info.name,
      lapsRequiredPercentage : info.lapsRequiredPercentage,
      racePointsDto : this.seasonForm.get('racePoints')!.value,
      qualPointsDto : this.seasonForm.get('qualPoints')!.value,
      fastestLapPointDto : fastestLapPoints[0] || { position : 10, points : 1},
      lobbySettingDto :  this.seasonForm.get('lobbySettings')!.value,
      assistDto : this.seasonForm.get('assists')!.value
    }
		//Add redirect from form to created Season page.
    console.log(seasonInsert);
    this.addSeason(seasonInsert);
	}

  getPropArray(value: string): Point[] {
    return this.seasonForm.get(value)?.value as Point[];
  }

  async addSeason(seasonInsert: SeasonInsert): Promise<void>{
    const result = await firstValueFrom(this.addSeasonService.addSeason(seasonInsert));
    console.log(result);
  }

	nextStep() {
		if (this.currentStep < this.totalSteps)
			this.currentStep++;
	}

	previousStep() {
		if (this.currentStep > 1)
			this.currentStep--;
	}

	get stepValidity() {
		return this.seasonForm?.get(this.stepFormNames[this.currentStep - 1])?.valid;
		// This can be improved by checking if all the steps before it are valid as well.
		// But right now, you can't skip the steps, so it doesn't mater until something like nonlinear "Material Stepper" is implemented
	}

	getControl(name: string): FormControl {
		return this.seasonForm.get(name) as FormControl;
	}

	checkError(error: string, ...name: string[]) {
		return name.some(el => this.getControl(el)?.hasError(error));
	}
}
