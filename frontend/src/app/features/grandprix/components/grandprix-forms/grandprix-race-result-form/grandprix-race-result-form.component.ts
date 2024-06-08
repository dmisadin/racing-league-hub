import { Component, Input } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { RaceResult, SessionResult } from 'app/shared/models/grandprix/Results';
import { Driver, Team } from 'app/shared/models/season/Season';
import { FormArray, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faUpDown, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { ResultStatus, SessionType } from 'app/shared/models/enums/Enumerations';
import { SessionPoints } from 'app/shared/models/season/SessionPoints';
import { IEnum, IEnumArray } from 'app/shared/models/interfaces';
import { GrandPrixService } from 'app/core/services/grand-prix.service';

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
    resultsSelectedForDeletion: RaceResult[] = [];
    faUpDown = faUpDown;
    faTrashCan = faTrashCan;
    ResultStatus: IEnumArray = Object.values(ResultStatus).slice(3, 8);
    SessionType: IEnum = SessionType;
    grandPrixId = 0;
    maxAmount = 22;

    constructor(private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private grandPrixService: GrandPrixService) { }

    raceResultFrom = this.fb.group({
        results: this.fb.array([]),
    })

    ngOnInit() {
        this.grandPrixId = this.route.snapshot.params['grandPrixId'];
        this.session.forEach((el) => {
            this.addResult(el)
        });
    }

    get results() {
        return this.raceResultFrom.controls["results"] as FormArray;
    }

    addResultRowClick() {
        this.addResult();
    }

    removeResultRowClick(index: number) {
        this.resultsSelectedForDeletion.push({ ...this.results.at(index).value, selectedForDeletion: true });
        this.results.removeAt(index);
        this.updatePositionsAndPoints(undefined, index);
    }

    updateFastestLap(event: Event) {
        let resultArray: RaceResult[] = this.results.getRawValue(); //za vrijednosti i disableanih FormControla
        this.updatePoints(resultArray)
        this.results.patchValue(resultArray);
    }

    addResult(result?: RaceResult) {
        const resultRow = this.fb.group({
            id: [result?.id ?? 0],
            grandPrixId: [this.grandPrixId],
            teamId: [result?.teamId ?? ''],
            sessionType: [this.SessionType.Race.Value],
            driverId: [result?.driverId ?? ''],
            position: [{ value: result?.position ?? '', disabled: true }],
            raceTime: [result?.raceTime ?? ''],
            resultStatus: [result?.resultStatus ?? ''],
            timePenalty: [result?.timePenalty],
            postRaceTimePenalty: [result?.postRaceTimePenalty ?? ''],
            lapsCompleted: [result?.lapsCompleted ?? 0],
            gridPosition: [result?.gridPosition ?? 0],
            fastestLapInMs: [result?.fastestLapInMs ?? null],
            pointsGained: [{ value: result?.pointsGained ?? '', disabled: true }],
            isReserve: [result?.isReserve ?? false],
            usedTyres: [result?.usedTyres ?? ''],
            selectedForDeletion: [false]
        });

        this.results.push(resultRow);
    }

    drop(event: CdkDragDrop<string[]>) {
        this.updatePositionsAndPoints(event);
    }

    updatePositionsAndPoints(event?: CdkDragDrop<string[]>, removedIndex?: number) {
        let resultArray: RaceResult[] = this.results.getRawValue(); //za vrijednosti i disableanih FormControla
        let minIndex = 0, maxIndex = resultArray.length - 1;
        if (event) {
            minIndex = Math.min(event.previousIndex, event.currentIndex);
            maxIndex = Math.max(event.previousIndex, event.currentIndex);
            moveItemInArray(resultArray, event.previousIndex, event.currentIndex);
        }

        for (let i = removedIndex ?? minIndex; i <= maxIndex; i++) {
            resultArray[i].position = i + 1;
        }

        this.updatePoints(resultArray);

        this.results.patchValue(resultArray);
    }

    updatePoints(resultArray: RaceResult[]) {
        const positionWithFastestLap = this.findPositionWithFastestLap(resultArray);

        resultArray.forEach((el: RaceResult, i: number) => {
            el.pointsGained = this.calculatePointsGained(el.position, positionWithFastestLap);
        })
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

    submitResults() {
        var results = this.raceResultFrom.getRawValue().results as RaceResult[];
        console.log("submit results", results);
        results.push(...this.resultsSelectedForDeletion);
        console.log("submit results after concat", results);
        this.grandPrixService.InsertGrandPrixResults(results);
    }
}
