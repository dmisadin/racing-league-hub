import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AddLeagueService } from 'app/core/services/add-league.service';
import { LeagueInsert } from 'app/shared/models/league/LeagueInsert';
import { RegionListService } from 'app/core/services/region-list.service';
import { ColorPickerService } from 'ngx-color-picker';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { Region } from 'app/shared/models/Region';

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

    leagueForm = this.fb.group({
        name: ['', Validators.required],
        regionId: [1, Validators.required],
        description: '',
        leagueLogo: '',
        color: '#000',
        website: ['', Validators.pattern(this.regexUrl)],
        discord: ['', Validators.pattern(this.regexUrl)],
        youtube: ['', Validators.pattern(this.regexUrl)],
        twitch: ['', Validators.pattern(this.regexUrl)],
        facebook: ['', Validators.pattern(this.regexUrl)],
        instagram: ['', Validators.pattern(this.regexUrl)],
    })

    constructor(private fb: FormBuilder,
        private cpService: ColorPickerService,
        private addLeagueService: AddLeagueService,
        private regionListService: RegionListService,
        private router: Router) { }

    ngOnInit() {
        this.regionListService.fetchData().subscribe((data) => {
            this.regionList = data;
            console.log(this.regionList)
        })
    }

    public get color(): string {
        return this.leagueForm.controls['color'].value || '#000';
    }

    public set color(value: string) {
        this.leagueForm.controls['color'].setValue(value);
    }

    social = [
        { name: 'Website', placeholder: 'www.adria.gg' },
        { name: 'Discord', placeholder: 'discord.gg/esportadria' },
        { name: 'YouTube', placeholder: 'youtube.com/EsportAdria' },
        { name: 'Twitch', placeholder: 'twitch.tv/EsportAdria' },
        { name: 'Facebook', placeholder: 'facebook.com/EsportAdriagg' },
        { name: 'Instagram', placeholder: 'instagram.com/EsportAdria' },
    ]


    onSubmit(): void {
        this.isSubmitted = true;
        console.log("Submitted form: ", this.leagueForm.value, this.leagueForm.valid);

        var leagueInsert: LeagueInsert;
        leagueInsert = {
            regionId: this.leagueForm.value.regionId!,
            name: this.leagueForm.value.name!,
            description: this.leagueForm.value.description!,
            colorHex: this.leagueForm.value.color!,
            imagePath: this.leagueForm.value.leagueLogo,
            website: this.leagueForm.value.website,
            discord: this.leagueForm.value.discord,
            youtube: this.leagueForm.value.youtube,
            twitch: this.leagueForm.value.twitch,
            facebook: this.leagueForm.value.facebook,
            instagram: this.leagueForm.value.instagram
        }
        this.addLeague(leagueInsert)
            .then(res => {
                // Redirect user to the created League page
                this.router.navigate(['leagues/', res]);
            }).catch(err => {
                console.log(err)
                // Redirect user to the home page. TO DO: Fail page
                this.router.navigate(['']);
            });
    }

    async addLeague(leagueInsert: LeagueInsert): Promise<number> {
        const result: number = await firstValueFrom(this.addLeagueService.addLeague(leagueInsert));
        return result;
    }
}

