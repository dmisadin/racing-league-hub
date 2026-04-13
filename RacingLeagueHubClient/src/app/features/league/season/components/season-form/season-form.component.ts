import { Component, inject, input, OnInit, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RestService } from '../../../../../core/services/rest.service';
import { RouteService } from '../../../../../core/services/route.service';
import { timezoneOptions } from '../../../../../shared/utilities/date.utility';
import { enumToOptions } from '../../../../../shared/utilities/enum.utility';
import { slugValidator } from '../../../../../shared/validators/slug.validator';
import { LeagueDto, Region } from '../../../models/league.model';
import { Platform, SeasonDto } from '../../models/season.model';
import { Game } from '../../../../../shared/models/enums';
import { InputTextComponent } from "../../../../../shared/components/input-fields/input-text/input-text.component";
import { NgSelectComponent } from "@ng-select/ng-select";
import { SlugPipe } from "../../../../../shared/pipes/slug.pipe";
import { InputNumberComponent } from "../../../../../shared/components/input-fields/input-number/input-number.component";
import { InputFileComponent } from '../../../../../shared/components/input-fields/input-file/input-file.component';
import { ToastService } from '../../../../../core/services/toast.service';
import { ListService } from '../../../../../shared/services/list.service';

@Component({
    selector: 'season-form',
    imports: [ReactiveFormsModule, InputTextComponent, NgSelectComponent, InputNumberComponent, InputFileComponent, SlugPipe],
    providers: [RouteService],
    templateUrl: './season-form.component.html'
})
export class SeasonFormComponent implements OnInit {
    private readonly routeService = inject(RouteService);
    private readonly restService = inject(RestService);
    private readonly toastService = inject(ToastService);
    private readonly listService = inject(ListService);
    private readonly fb = inject(FormBuilder);

    season = input<SeasonDto | null>();
    cancel = output();

    form: FormGroup;
    platformOptions = enumToOptions(Platform);
    gameOptions = enumToOptions(Game);

    constructor() {
        this.form = this.fb.group({
            id: [null],
            leagueId: [null, Validators.required],
            name: ["", Validators.required],
            platform: [Platform.Steam, [Validators.required, Validators.maxLength(5)]],
            game: [Game.F125, Validators.required],
            lapPercentageRequired: [90, [Validators.min(1), Validators.max(100)]],
            slug: ["", [Validators.required, Validators.maxLength(64), slugValidator]],
            logoResourceId: [null],
        });
    }

    ngOnInit(): void {
        const season = this.season();
        if (season) {
            this.form.patchValue(season);
            return;
        }

        const leagueSlug = this.routeService.getRouteParam("leagueSlug");
        const seasonSlug = this.routeService.getRouteParam("seasonSlug");
        if (!leagueSlug || !seasonSlug) {
            this.restService.get<LeagueDto>(`/leagues/${leagueSlug}`)
                            .subscribe(res => this.form.patchValue({leagueId: res.id}));
            return;
        }

        this.restService.get<SeasonDto>(`/leagues/${leagueSlug}/seasons/${seasonSlug}`)
            .subscribe(res => this.form.patchValue(res));
    }

    onSubmit() {
        if (this.form.invalid)
            return;

        const form = this.form.value;

        if (form['id'])
            this.restService.post('/seasons/update', this.form.value).subscribe({
                next: () => this.toastService.showSuccess("Successfully updated the season."),
                error: () => this.toastService.showError("Failed to update the season.")
            });
        else
            this.restService.post('/seasons/add', this.form.value).subscribe({
                next: () => this.onAddSuccess(),
                error: () => this.toastService.showError("Failed to add a new season.")
            });

    }

    onCancel() {
        this.cancel.emit();
    }

    onAddSuccess() {
        this.toastService.showSuccess("Successfully added a new season.");
        this.listService.triggerReload();
        this.routeService.navigateToParent();
    }
}
