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

@Component({
    selector: 'track-layout-form',
    imports: [ReactiveFormsModule, InputNumberComponent, InputTextComponent, NgSelectComponent],
    templateUrl: './track-layout-form.component.html',
})
export class TrackLayoutFormComponent implements OnInit {
    private readonly fb = inject(FormBuilder);
    private readonly routeService = inject(RouteService);
    private readonly restService = inject(RestService);

    trackLayoutId = input<number>();
    trackLayout = input<TrackLayoutDto | null>(null);
    cancel = output();

    form!: FormGroup;
    gameChoices: DropdownOption[] = enumToOptions(Game);

    ngOnInit(): void {
        const trackId = Number(this.routeService.getRouteParam("trackId"));
        
        this.form = this.fb.group({
            id: [null],
            trackId: [trackId, Validators.required],
            name: ["", Validators.required],
            pitStopDuration: [null],
            cornersTotal: [null],
            cornersLeft: [null, [Validators.min(0), maxFromControl('cornersTotal')]],
            lapsGrandPrix: [null],
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
            this.restService.post('/track-layout/update/' + form['id'], this.form.value).subscribe();
        else
            this.restService.post('/track-layout/add', this.form.value).subscribe();
    }

    onCancel() {
        this.cancel.emit();
        //reset form
    }
}
