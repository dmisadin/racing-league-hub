import { Component, inject, input, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RestService } from '../../../../../../core/services/rest.service';
import { RouteService } from '../../../../../../core/services/route.service';
import { ToastService } from '../../../../../../core/services/toast.service';
import { ListService } from '../../../../../../shared/services/list.service';
import { slugValidator } from '../../../../../../shared/validators/slug.validator';
import { SeasonDto } from '../../../models/season.model';
import { GrandPrixDto } from '../../models/grand-prix.model';
import { InputTextComponent } from "../../../../../../shared/components/input-fields/input-text/input-text.component";
import { SlugPipe } from "../../../../../../shared/pipes/slug.pipe";
import { ApiInputSelectComponent } from "../../../../../../shared/components/input-fields/api-input-select/api-input-select.component";
import { toUtcIso } from '../../../../../../shared/utilities/date.utility';

@Component({
    selector: 'grand-prix-form',
    imports: [ReactiveFormsModule, InputTextComponent, SlugPipe, ApiInputSelectComponent],
    providers: [RouteService],
    templateUrl: './grand-prix-form.component.html'
})
export class GrandPrixFormComponent {
    private readonly routeService = inject(RouteService);
    private readonly restService = inject(RestService);
    private readonly toastService = inject(ToastService);
    private readonly listService = inject(ListService);
    private readonly fb = inject(FormBuilder);

    grandPrix = input<GrandPrixDto | null>();
    cancel = output();

    form: FormGroup;

    constructor() {
        this.form = this.fb.group({
            id: [null],
            seasonId: [null, Validators.required],
            trackLayoutId: [null, Validators.required],
            name: ["", Validators.required],
            startingAt: ["", Validators.required],
            vodUrl: [null],
            slug: ["", [Validators.required, Validators.maxLength(64), slugValidator]]
        });
    }

    ngOnInit(): void {
        const grandPrix = this.grandPrix();
        if (grandPrix) {
            this.form.patchValue(grandPrix);
            return;
        }

        const leagueSlug = this.routeService.getRouteParam("leagueSlug");
        const seasonSlug = this.routeService.getRouteParam("seasonSlug");
        const grandPrixSlug = this.routeService.getRouteParam("grandPrixSlug");

        if (!grandPrixSlug && seasonSlug && leagueSlug) {
            this.restService.get<SeasonDto>(`/leagues/${leagueSlug}/seasons/${seasonSlug}`)
                .subscribe(res => this.form.patchValue({ seasonId: res.id }));
            return;
        }
        
        this.restService.get<GrandPrixDto>(`/leagues/${leagueSlug}/seasons/${seasonSlug}/grands-prix/${grandPrixSlug}`)
            .subscribe(res => this.form.patchValue(res));
    }

    onSelect(value: string) {
        this.form.patchValue({ trackLayoutId: value });
    }

    onSubmit() {
        if (this.form.invalid)
            return;

        const form = this.form.value;
        form.startingAt = toUtcIso(form.startingAt);

        if (form['id'])
            this.restService.post('/grands-prix/update', this.form.value).subscribe({
                next: () => this.toastService.showSuccess("Successfully updated the Grand Prix."),
                error: () => this.toastService.showError("Failed to update the Grand Prix.")
            });
        else
            this.restService.post('/grands-prix/add', this.form.value).subscribe({
                next: () => this.onAddSuccess(),
                error: () => this.toastService.showError("Failed to add a new Grand Prix.")
            });

    }

    onCancel() {
        this.cancel.emit();
    }

    onAddSuccess() {
        this.toastService.showSuccess("Successfully added a new Grand Prix.");
        this.listService.triggerReload();
        this.routeService.navigateToParent();
    }
}
