import { Component, Input } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { RaceResult } from 'app/shared/models/grandprix/Results';
import { Driver, Team } from 'app/shared/models/season/Season';
import { AbstractControl, ControlContainer, FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faUpDown } from '@fortawesome/free-solid-svg-icons';
import { resultStatus } from 'app/shared/models/resultStatus';

@Component({
    selector: 'app-grandprix-race-result-form',
    templateUrl: './grandprix-race-result-form.component.html',
    styleUrls: ['./grandprix-race-result-form.component.scss']
})
export class GrandprixRaceResultFormComponent {
    @Input() session: RaceResult[] = [];
    @Input() drivers: Driver[] = [];
    @Input() teams: Team[] = [];
    faUpDown = faUpDown;
    resultStatus = Object.values(resultStatus);
    movies = [
        { position: 1, ime: "magija" },
        { position: 2, ime: "shrooom" },
        { position: 3, ime: "Dux" },
    ];
    grandprixId = 0;

    constructor(private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,) { }

    raceResultFrom = this.fb.group({
        results: this.fb.array([]),
    })

    ngOnInit() {
        console.log(this.session)
        this.grandprixId = this.route.snapshot.params['grandprixId'];
        this.session.forEach((el) => {
            this.addResult(el)
        });
        console.log(this.resultStatus)
    }

    get results() {
        return this.raceResultFrom.controls["results"] as FormArray;
    }

    addResult(result: RaceResult) {
        const resultRow = this.fb.group({
            grandprixId: [this.grandprixId],
            teamId: [result.teamId],
            driverId: [result.driverId],
            position: [result.position],
            raceTime: [result.raceTime],
            resultStatus: [result.resultStatus],
            timePenalty: [result.timePenalty],
            postRaceTimePenalty: [result.postRaceTimePenalty],
            lapsCompleted: [result.lapsCompleted],
            gridPosition: [result.gridPosition],
            fastestLapInMs: [result.fastestLapInMs],
            pointsGained: [result.pointsGained],
            isReserve: [result.isReserve],
            usedTyres: [result.usedTyres],
        });
        this.results.push(resultRow);
    }

    drop(event: CdkDragDrop<string[]>) {
        moveItemInArray(this.movies, event.previousIndex, event.currentIndex);

        this.session.forEach((el, i) => el.position = i + 1)
    }
}

/*
{
            grandprixId: [this.grandprixId],
            teamId: [0],
            driverId: [0],
            position: [0],
            raceTime: [0.0],
            resultStatus: [3],
            timePenalty: [0],
            postRaceTimePenalty: [0],
            lapsCompleted: [0],
            gridPosition: [0],
            fastestLapInMS:  [0],
            pointsGained: [0],
            isReserve: [false],
            usedTyres: [""],
        }
*/