import { Component } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { GrandprixInfoItemComponent } from './grandprix-info-item/grandprix-info-item.component';
import { GrandPrixInsert } from 'app/shared/models/grandprix/GrandPrixInsert';
import { Subscription, firstValueFrom } from 'rxjs';
import { AddGrandprixService } from 'app/features/grandprix/services/add-grandprix.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
    selector: 'app-grandprix-forms',
    templateUrl: './grandprix-forms.component.html',
    styleUrls: ['./grandprix-forms.component.scss']
})
export class GrandPrixFormsComponent {
    isSubmitted: boolean = false;
    infoValue = { name: "", startTime: "", hasSprint: false, youTubeURL: 90, trackId: 0, countryId: 0 };
    maxAmount = 25;
    gpItem$!: Subscription;
    gpId: number = 0;
    seasonId: number = 0;
    leagueId: number = 0;
    grandPrixForm = this.fb.group({
        list: this.fb.array([])
    });

    constructor(private fb: FormBuilder, 
        private addGrandPrixService: AddGrandprixService, 
        private route: ActivatedRoute,
        private router: Router) { }

    onSubmit(): void {
        this.isSubmitted = true;
        console.log("Submitted form: ", this.grandPrixForm.value);
        //Add redirect from form to Season page.
        var value = this.getFormArray('list').value;

        var grandPrixInsert: Array<GrandPrixInsert> = new Array<GrandPrixInsert>();
        value.forEach((item: GrandPrixInsert) => {
            grandPrixInsert.push(item);
        });

        grandPrixInsert.forEach((item: GrandPrixInsert) => {
            item.seasonId = this.seasonId;
        })

        this.addGrandPrix(grandPrixInsert).then(res => {
            // Redirect user to the updated League page (or refresh?); success!
            this.router.navigate(['leagues', this.leagueId, 'season', this.seasonId]);
        }).catch(err => {
            console.log(err)
            // Redirect user to the home page. TO DO: Fail page ili alert service
            this.router.navigate(['']);
        });
        console.log(grandPrixInsert);

    }

    ngOnInit() {
        //console.log(this.getFormArray('list').controls.length);
        this.gpItem$ = this.route.params.subscribe(params => {
            //this.gpId = params['grandPrixId'];
            this.seasonId = params['seasonId'];
            this.leagueId = params['leagueId'];
        })
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

    async addGrandPrix(grandPrixInserts: Array<GrandPrixInsert>): Promise<number> {
        const result: number = await firstValueFrom(this.addGrandPrixService.addGrandPrix(grandPrixInserts));
        console.log(result);
        return result;
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
