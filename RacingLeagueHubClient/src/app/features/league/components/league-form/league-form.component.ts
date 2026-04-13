import { Component, inject, input, OnInit, output } from '@angular/core';
import { RouteService } from '../../../../core/services/route.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LeagueDto, Region } from '../../models/league.model';
import { RestService } from '../../../../core/services/rest.service';
import { enumToOptions } from '../../../../shared/utilities/enum.utility';
import { InputTextComponent } from "../../../../shared/components/input-fields/input-text/input-text.component";
import { NgSelectComponent } from '@ng-select/ng-select';
import { InputFileComponent } from '../../../../shared/components/input-fields/input-file/input-file.component';
import { slugValidator } from '../../../../shared/validators/slug.validator';
import { SlugPipe } from "../../../../shared/pipes/slug.pipe";
import { timezoneOptions } from '../../../../shared/utilities/date.utility';
import { ToastService } from '../../../../core/services/toast.service';
import { ListService } from '../../../../shared/services/list.service';

@Component({
    selector: 'league-form',
    imports: [ReactiveFormsModule, NgSelectComponent, InputTextComponent, InputFileComponent, SlugPipe],
    providers: [RouteService],
    templateUrl: './league-form.component.html',
})
export class LeagueFormComponent implements OnInit {
    private readonly routeService = inject(RouteService);
    private readonly restService = inject(RestService);
    private readonly toastService = inject(ToastService);
    private readonly listService = inject(ListService);
    private readonly fb = inject(FormBuilder);

    league = input<LeagueDto | null>();
    cancel = output();

    form: FormGroup;
    regionOptions = enumToOptions(Region);
    timezoneOptions = timezoneOptions;

    constructor() {
        this.form = this.fb.group({
            id: [null],
            name: ["", Validators.required],
            abbreviation: ["", [Validators.required, Validators.maxLength(5)]],
            region: [0, Validators.required],
            description: ["", Validators.maxLength(300)],
            slug: ["", [Validators.required, Validators.maxLength(64), slugValidator]],
            timezone: [Intl.DateTimeFormat().resolvedOptions().timeZone, Validators.required],
            logoResourceId: [""],
        });
    }

    ngOnInit(): void {
        const league = this.league();
        if (league) {
            this.form.patchValue(league);
            return;
        }

        const leagueSlug = this.routeService.getRouteParam("leagueSlug");
        if (!leagueSlug)
            return;

        this.restService.get<LeagueDto>(`/leagues/${leagueSlug}`)
                        .subscribe(res => this.form.patchValue(res));
    }

    onSubmit() {
        if (this.form.invalid)
            return;

        const form = this.form.value;
        
        if (form['id'])
            this.restService.post('/leagues/update', this.form.value).subscribe({
                next: () => this.toastService.showSuccess("Successfully updated the league."),
                error: () => this.toastService.showError("Failed to update the league.")
            });
        else
            this.restService.post('/leagues/add', this.form.value).subscribe({
                next: () => this.onAddSuccess(),
                error: () => this.toastService.showError("Failed to add a new league.")
            });

    }

    onCancel() {
        this.cancel.emit();
    }

    onAddSuccess() {
        this.toastService.showSuccess("Successfully added a new league.");
        this.listService.triggerReload();
        this.routeService.navigateToParent();
    }
}
