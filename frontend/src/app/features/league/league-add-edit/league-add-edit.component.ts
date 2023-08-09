import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AddLeagueService } from 'app/core/services/add-league.service';
import { LeagueInsert } from 'app/shared/models/league/LeagueInsert';
import { RegionListService } from 'app/core/services/region-list.service';
//import { ColorPickerService } from 'ngx-color-picker';
import { firstValueFrom, switchMap } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { Region } from 'app/shared/models/Region';
import { LeagueDataService } from 'app/core/services/league-data.service';

@Component({
    selector: 'app-league-add-edit',
    templateUrl: './league-add-edit.component.html',
    styleUrls: ['./league-add-edit.component.scss']
})

/**
 * TO DO:
 *  - Create service for uploading image to backend and store URL to database(ImagePath)
 *  - Add image preview https://github.com/bezkoder/angular-14-image-upload-preview
 */
export class LeagueAddEditComponent {
    regionList: Region[] = [];
    isSubmitted: boolean = false;
    regexUrl = '(https?://)?([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';
    preview = '';
    title = "Create new League";
    buttonText = "Create"
    id?: number;
    loading = false;

    constructor(private fb: FormBuilder,
        //private cpService: ColorPickerService,
        private addLeagueService: AddLeagueService,
        private leagueDataService: LeagueDataService,
        private regionListService: RegionListService,
        private router: Router,
        private route: ActivatedRoute,
    ) { }

    leagueForm = this.fb.group({
        name: ['', Validators.required],
        regionId: [1, Validators.required],
        description: '',
        leagueLogo: '',
        color: '#000000',
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
            this.leagueDataService.getById(this.id)
                .subscribe(data => {
                    console.log(data)
                    this.title = "Edit " + data.name;
                    this.buttonText = "Edit";
                    this.leagueForm.patchValue(data);
                    this.loading = false;
                    this.color = data.colorHex;
                    this.leagueForm.markAsPristine();
                    this.subToChanges();
                });
        }

    }

    public get color(): string {
        return this.leagueForm.controls['color'].value || "#000000";
    }

    public set color(value: string) {
        this.leagueForm.controls['color'].setValue(value);
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

        var leagueInsert: LeagueInsert;
        leagueInsert = {
            regionId: this.leagueForm.value.regionId!,
            name: this.leagueForm.value.name!,
            description: this.leagueForm.value.description!,
            colorHex: this.leagueForm.value.color!,
            imagePath: this.leagueForm.value.leagueLogo,
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

    async addLeague(leagueInsert: LeagueInsert): Promise<number> {
        const result: number = await firstValueFrom(this.addLeagueService.addLeague(leagueInsert));
        return result;
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

}

