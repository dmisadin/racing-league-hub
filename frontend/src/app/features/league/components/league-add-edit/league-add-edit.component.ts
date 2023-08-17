import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AddLeagueService } from 'app/features/league/services/add-league.service';
import { LeagueInsert } from 'app/shared/models/league/LeagueInsert';
import { RegionListService } from 'app/core/services/region-list.service';
//import { ColorPickerService } from 'ngx-color-picker';
import { firstValueFrom, switchMap } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { Region } from 'app/shared/models/Region';
import { LeagueDataService } from 'app/features/league/services/league-data.service';
import { JsonPatch } from 'app/shared/models/JsonPatch';
import { LeaguePatchService } from 'app/features/league/services/league-patch.service';
import { faCircleXmark } from '@fortawesome/free-solid-svg-icons';
import { ButtonClickService } from 'app/core/services/button-click.service';

@Component({
    selector: 'app-league-add-edit',
    templateUrl: './league-add-edit.component.html',
    styleUrls: ['./league-add-edit.component.scss']
})

/**
 * TO DO:
 *  - Create service for uploading image to backend and store URL to database(ImagePath)
 *  - Add image preview https://github.com/bezkoder/angular-14-image-upload-preview
 *  - Listen if there are any fields that are not "pristine", warn user there are unsaved stuff
 */
export class LeagueAddEditComponent {
    regionList: Region[] = [];
    isSubmitted: boolean = false;
    isEditMode: boolean = false;
    faCircleXmark = faCircleXmark;
    loading = false;
    regexUrl = '(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';
    preview = '';
    title = "Create new League";
    buttonText = "Create"
    id!: number;
    patchData: JsonPatch[] = [];

    constructor(private fb: FormBuilder,
        //private cpService: ColorPickerService,
        private addLeagueService: AddLeagueService,
        private leagueDataService: LeagueDataService,
        private regionListService: RegionListService,
        private leaguePatchService: LeaguePatchService,
        private buttonClickService: ButtonClickService,
        private router: Router,
        private route: ActivatedRoute,
    ) { }

    leagueForm = this.fb.group({
        name: ['', Validators.required],
        regionId: [1, Validators.required],
        description: '',
        imagePath: '',
        colorHex: '#000000',
        socialMedia: this.fb.group({
            website: ['', Validators.pattern(this.regexUrl)],
            discord: ['', Validators.pattern(this.regexUrl)],
            youtube: [''],
            twitch: [''],
            facebook: [''],
            instagram: [''],
            twitter: [''],
        }),
    })

    ngOnInit() {
        this.regionListService.fetchData().subscribe((data) => {
            this.regionList = data;
            console.log(this.regionList)
        })

        this.id = this.route.snapshot.params['id'];
        this.route.parent!.params.subscribe(params => {
            this.id = params['id'];
        });

        if (this.id) {
            // edit mode
            this.loading = true;
            this.isEditMode = true;
            this.leagueDataService.getById(this.id)
                .subscribe(data => {
                    console.log(data)
                    this.title = "Edit " + data.name;
                    this.buttonText = "Edit";
                    this.leagueForm.patchValue({...data, regionId: data.region?.id});
                    this.loading = false;
                    this.color = data.colorHex;
                    this.leagueForm.markAsPristine();
                    console.log(this.leagueForm)
                    //this.subToChanges();
                });
        }

    }

    public get color(): string {
        return this.leagueForm.controls['colorHex'].value!;
    }

    public set color(value: string) {
        this.leagueForm.controls['colorHex'].setValue(value);
        this.leagueForm.controls['colorHex'].markAsDirty();
    }

    subToChanges() {
        this.leagueForm.valueChanges.subscribe(data => {
            console.log('All changes', data);
        })

        this.leagueForm.get('socialMedia')!.valueChanges.subscribe(data => {
            console.log('Social changes', data);
        })
    }

    onSubmit(): void {
        this.isSubmitted = true;
        console.log("Submitted form: ", this.leagueForm);

        if (!this.isEditMode) {
            var leagueInsert: LeagueInsert;
            leagueInsert = {
                regionId: this.leagueForm.value.regionId!,
                name: this.leagueForm.value.name!,
                description: this.leagueForm.value.description!,
                colorHex: this.leagueForm.value.colorHex!,
                imagePath: this.leagueForm.value.imagePath,
                ...this.leagueForm.value.socialMedia
            }
            console.log(leagueInsert)
            this.addLeague(leagueInsert)
                .then(res => {
                    // Redirect user to the created League page
                    this.router.navigate(['leagues', res]);
                }).catch(err => {
                    console.log(err)
                    // Redirect user to the home page. TO DO: Fail page ili alert service
                    this.router.navigate(['']);
                });
        }
        else {
            this.getUpdates(this.leagueForm);
            this.updateLeague(this.patchData).then(res => {
                // Redirect user to the updated League page (or refresh?)
                this.router.navigate(['leagues', this.id]); //trenutno ne povuce nove podatke!
            }).catch(err => {
                console.log(err)
                // Redirect user to the home page. TO DO: Fail page ili alert service
                this.router.navigate(['']);
            });
            console.log(this.patchData);
        }
    }

    async addLeague(leagueInsert: LeagueInsert): Promise<number> {
        const result: number = await firstValueFrom(this.addLeagueService.addLeague(leagueInsert));
        return result;
    }

    async updateLeague(data: JsonPatch[]): Promise<number> {
        const result: number = await firstValueFrom(this.leaguePatchService.patch(this.id, data));
        return result;
    }


    private getUpdates(formItem: FormGroup | FormArray | FormControl, name: string = '') {

        if (formItem instanceof FormControl) {
            if (name && formItem.dirty) {
                console.log(name)
                this.patchData.push({
                    path: name,
                    op: "replace",
                    value: formItem.value
                })
            }
        }
        else {
            for (const formControlName in formItem.controls) {
                if (formItem.controls.hasOwnProperty(formControlName)) {
                    const formControl = formItem.get(formControlName);

                    console.log(name)
                    console.log(formControlName)
                    if (formControl instanceof FormControl) {
                        this.getUpdates(formControl, name + '/' + formControlName);
                    }
                    else if (formControl instanceof FormGroup && formControl.dirty) {
                        this.getUpdates(formControl, name + '/' + formControlName);
                    }
                }
            }
        }
    }

    triggerButtonClick() {
        this.buttonClickService.buttonClicked.next(false);
    }
    social = [
        { name: 'Website', placeholder: 'adria.gg/', left: "86px" },
        { name: 'Discord', placeholder: 'discord.gg/esportadria', left: "102px" },
        { name: 'YouTube', placeholder: 'youtube.com/EsportAdria', left: "120px" },
        { name: 'Twitch', placeholder: 'twitch.tv/EsportAdria', left: "88px" },
        { name: 'Facebook', placeholder: 'facebook.com/EsportAdriagg', left: "130px" },
        { name: 'Instagram', placeholder: 'instagram.com/EsportAdria', left: "134px" },
        { name: 'Twitter', placeholder: 'twitter.com/EsportAdria', left: "108px" },
    ]

    closeEditForm() {
        this.router.navigate(['../'], { relativeTo: this.route });
        this.isEditMode = !this.isEditMode;
    }
}


/*                     FormArray is not needed here, but something to think about later
                    else if (formControl instanceof FormArray && formControl.dirty && formControl.controls.length > 0) {
                        updatedValues[formControlName] = [];
                        this.getUpdates(formControl, updatedValues[formControlName]);
                    } */