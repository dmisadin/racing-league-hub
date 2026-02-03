import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { AddSeasonService } from 'app/features/season/services/add-season.service';
import { Point } from 'app/shared/models/season/Point';
import { Assists } from 'app/shared/models/season/Assists';
import { Info } from 'app/shared/models/season/Info';
import { LobbySettings } from 'app/shared/models/season/LobbySettings';
import { SeasonInsert } from 'app/shared/models/season/SeasonInsert';
import { Subscription, firstValueFrom } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { PointsType } from 'app/shared/models/enums/Enumerations';

@Component({
    selector: 'app-season-forms',
    templateUrl: './season-forms.component.html',
    styleUrls: ['./season-forms.component.scss']
})
export class SeasonFormsComponent {
    faArrowLeft = faArrowLeft;
    faArrowRight = faArrowRight;
    PointsType = PointsType;

    currentStep = 1;
    totalSteps = 8;
    preview = '';
    isSubmitted: boolean = false;
    season$!: Subscription;
    leagueId: number = 0;
    stepFormNames = ["seasonInfo", "seasonPoints", "seasonPoints", "seasonPoints", "seasonPoints", "lobbySettings", "assists"]
    infoValue = { name: "", gameId: 5, platformId: 1, lapsRequiredPercentage: 90 };

    qualPointsArray = this.fb.array([]);
    sprintPointsArray = this.fb.array([]);
    racePointsArray = this.fb.array([]);
    fastestLapPointsArray = this.fb.array([]);

    seasonForm = this.fb.group({
        seasonPoints: this.fb.array([])
        // The rest of form group is added inside child components with parent.addControl()
    });

    constructor(private fb: FormBuilder, 
        private addSeasonService: AddSeasonService, 
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {
        this.season$ = this.route.params.subscribe(params => {
            this.leagueId = params['leagueId'];
        })
    }

    onSubmit(): void {
        this.isSubmitted = true;
        console.log("Submitted form: ", this.seasonForm.value, this.seasonForm.valid, this.seasonForm);
        //var fastestLapPoints: Point[] = this.seasonForm.get('fastestLapPoints')!.value;
        const seasonInfo: Info  = this.seasonForm.get('seasonInfo')!.value;
        const qualifyingPoints: Point[] = this.seasonForm.get('points' + this.PointsType.Qualifying.ValueName)?.value;
        const sprintPoints: Point[] = this.seasonForm.get('points' + this.PointsType.Sprint.ValueName)?.value;
        const racePoints: Point[] = this.seasonForm.get('points' + this.PointsType.Race.ValueName)?.value;
        const fastestLapPoints: Point[] = this.seasonForm.get('points' + this.PointsType.FastestLap.ValueName)?.value;

        var seasonInsert: SeasonInsert;
        seasonInsert = {
            leagueId: this.leagueId,
            game: seasonInfo.game,
            platform: seasonInfo.platform,
            imagePath: '',
            name: seasonInfo.name,
            lapsRequiredPercentage: seasonInfo.lapsRequiredPercentage,
            seasonPoints: [...qualifyingPoints, ...sprintPoints, ...racePoints, ...fastestLapPoints],
            lobbySettings: this.seasonForm.get('lobbySettings')?.value,
            assists: this.seasonForm.get('assists')!.value
        }
        //Add redirect from form to created Season page.
        console.log(seasonInsert);
        this.addSeason(seasonInsert).then(() => {
            this.router.navigate([`/leagues/${this.leagueId}`]);
        });
    }

    getPropArray(value: string): Point[] {
        return this.seasonForm.get(value)?.value as Point[];
    }

    async addSeason(seasonInsert: SeasonInsert): Promise<void> {
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

    get pointsFormArray() {
        return this.seasonForm.get("seasonPoints") as FormArray;
    }

    getControl(name: string): FormControl {
        return this.seasonForm.get(name) as FormControl;
    }

    checkError(error: string, ...name: string[]) {
        return name.some(el => this.getControl(el)?.hasError(error));
    }
}
