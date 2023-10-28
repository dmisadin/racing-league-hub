import { Component, Input } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { RaceResult } from 'app/shared/models/grandprix/Results';
import { Driver, Team } from 'app/shared/models/season/Season';
import { AbstractControl, ControlContainer, FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faUpDown } from '@fortawesome/free-solid-svg-icons';
import { resultStatus } from 'app/shared/models/resultStatus';
import { SessionPoints } from 'app/shared/models/season/SessionPoints';

@Component({
    selector: 'app-grandprix-race-result-form',
    templateUrl: './grandprix-race-result-form.component.html',
    styleUrls: ['./grandprix-race-result-form.component.scss']
})
export class GrandprixRaceResultFormComponent {
    @Input() session: RaceResult[] = [];
    @Input() drivers: Driver[] = [];
    @Input() teams: Team[] = [];
    @Input() sessionPoints?: SessionPoints;
    faUpDown = faUpDown;
    resultStatus = Object.values(resultStatus).slice(3, 8);
    grandprixId = 0;

    constructor(private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,) { }

    raceResultFrom = this.fb.group({
        results: this.fb.array([]),
    })

    ngOnInit() {
        this.grandprixId = this.route.snapshot.params['grandprixId'];
        this.session.forEach((el) => {
            this.addResult(el)
        });

        if (this.sessionPoints) {

        }
    }

    get results() {
        return this.raceResultFrom.controls["results"] as FormArray;
    }

    addResult(result: RaceResult) {
        const resultRow = this.fb.group({
            id: [result.id],
            grandprixId: [this.grandprixId],
            teamId: [result.teamId],
            driverId: [result.driverId],
            position: [{value: result.position, disabled: true}],
            raceTime: [result.raceTime],
            resultStatus: [result.resultStatus],
            timePenalty: [result.timePenalty],
            postRaceTimePenalty: [result.postRaceTimePenalty],
            lapsCompleted: [result.lapsCompleted],
            gridPosition: [result.gridPosition],
            fastestLapInMs: [result.fastestLapInMs],
            pointsGained: [{value: result.pointsGained, disabled: true}],
            isReserve: [result.isReserve],
            usedTyres: [result.usedTyres],
        });
        this.results.push(resultRow);
    }

    drop(event: CdkDragDrop<string[]>) {
        let formArrayValue: RaceResult[] = this.results.getRawValue(); //za vrijednosti i disableanih FormControla
        const minIndex = Math.min(event.previousIndex, event.currentIndex);
        const maxIndex = Math.max(event.previousIndex, event.currentIndex);
        console.log(minIndex, maxIndex)

        moveItemInArray(formArrayValue, event.previousIndex, event.currentIndex);
        
        for (let i = minIndex; i <= maxIndex; i++) {
            formArrayValue[i].position = i + 1;
        }
        const positionWithFastestLap = this.findPositionWithFastestLap(formArrayValue);

        formArrayValue.forEach((el: RaceResult, i: number) => {
            el.pointsGained = this.calculatePointsGained(el.position, positionWithFastestLap);
        })


        //let formValueSorted = this.results.value.sort((a: RaceResult, b: RaceResult) => a.position - b.position);
        this.results.patchValue(formArrayValue);
        console.log("TEST PATCH VALUE ", this.results.value)
    }

    findPositionWithFastestLap(results: RaceResult[]): number {
        const raceResult = results.reduce((prev, curr) => {
            if (curr.fastestLapInMs && prev.fastestLapInMs)
                return curr.fastestLapInMs < prev.fastestLapInMs ? curr : prev;
            else if (!curr.fastestLapInMs)
                return prev;
            else
                return curr;
        });
        return raceResult.position;
    }

    calculatePointsGained(position: number, positionWithFastestLap: number) {
        let pointsSum = 0;
        if (this.sessionPoints?.racePoints)
            pointsSum += this.sessionPoints.racePoints.find(rp => rp.position === position)?.points || 0;

        if (this.sessionPoints?.fastestLapPoints
            && position === positionWithFastestLap
            && position <= this.sessionPoints.fastestLapPoints.position) {
            pointsSum += this.sessionPoints.fastestLapPoints.points;
        }
        return pointsSum;
    }
}