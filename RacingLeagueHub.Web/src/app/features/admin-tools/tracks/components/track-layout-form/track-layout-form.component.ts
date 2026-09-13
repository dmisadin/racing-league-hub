import { Component, inject, input, OnInit, output } from "@angular/core";
import { TrackLayoutDto } from "../../models/track.model";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { InputNumberComponent } from "../../../../../shared/components/input-fields/input-number/input-number.component";
import { InputTextComponent } from "../../../../../shared/components/input-fields/input-text/input-text.component";
import { maxFromControl } from "../../../../../shared/validators/maxFromControl.validator";
import { NgSelectComponent } from "@ng-select/ng-select";
import { DropdownOption } from "../../../../../shared/models/models";
import { enumToOptions } from "../../../../../shared/utilities/enum.utility";
import { Game } from "../../../../../shared/models/enums";
import { RestService } from "../../../../../core/services/rest.service";
import { RouteService } from "../../../../../core/services/route.service";
import { ListService } from "../../../../../shared/services/list.service";
import { ToastService } from "../../../../../core/services/toast.service";

@Component({
    selector: 'track-layout-form',
    imports: [ReactiveFormsModule, InputNumberComponent, InputTextComponent, NgSelectComponent],
    providers: [RouteService],
    templateUrl: './track-layout-form.component.html',
})
export class TrackLayoutFormComponent implements OnInit {
    private readonly fb = inject(FormBuilder);
    private readonly routeService = inject(RouteService);
    private readonly restService = inject(RestService);
    private readonly listService = inject(ListService);
        private readonly toastService = inject(ToastService);

    trackLayoutId = input<string>();
    trackLayout = input<TrackLayoutDto | null>(null);
    cancel = output();

    form!: FormGroup;
    gameChoices: DropdownOption[] = enumToOptions(Game);

    ngOnInit(): void {
        const trackId = this.routeService.getRouteParam("trackId");

        this.form = this.fb.group({
            id: [null],
            trackId: [trackId, Validators.required],
            name: ["", Validators.required],
            pitStopDuration: [null],
            cornersTotal: [null, Validators.required],
            cornersLeft: [null, [Validators.min(0), maxFromControl('cornersTotal')]],
            lapsGrandPrix: [null, Validators.required],
            length: [null, Validators.required],
            elevationChange: [null, Validators.required],
            telemetryId: [null, Validators.required],
            trackLayoutGames: [[]]
        });

        const layout = this.trackLayout();

        if (layout) {
            this.form.patchValue(layout);
        } else if (this.trackLayoutId()) {
            this.loadLayout();
        }
    }

    private loadLayout() {
        this.restService
            .get<TrackLayoutDto>('/track-layout/get-by-id/' + this.trackLayoutId())
            .subscribe(layout => {
                this.form.patchValue(layout);
            });
    }

    saveAllChanges() {
        if (this.form.invalid)
            return;

        const form = this.form.value;
        if (form['id'])
            this.restService.put(`/track-layout/${form['id']}`, this.form.value).subscribe({
                next: () => this.toastService.showSuccess("Successfully updated the track layout."),
                error: () => this.toastService.showError("Failed to update the track layout.")
            });
        else
            this.restService.post('/track-layout', this.form.value).subscribe(() => this.onAddSuccess());
    }

    onCancel() {
        this.cancel.emit();
        //reset form
    }

    onAddSuccess() {
        this.toastService.showSuccess("Successfully added a new track layout.");
        this.listService.triggerReload();
        this.routeService.navigateToParent();
    }
}
