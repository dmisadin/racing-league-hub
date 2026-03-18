import { Component, inject, input, OnInit, output } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { CountryPickerComponent } from "../../../../../shared/components/input-fields/country-picker/country-picker.component";
import { TrackDto } from "../../models/track.model";
import { InputTextComponent } from "../../../../../shared/components/input-fields/input-text/input-text.component";
import { RestService } from "../../../../../core/services/rest.service";
import { RouteService } from "../../../../../core/services/route.service";
import { ListService } from "../../../../../shared/services/list.service";

@Component({
    selector: 'track-form',
    imports: [ReactiveFormsModule, CountryPickerComponent, InputTextComponent],
    providers: [RouteService],
    templateUrl: './track-form.component.html',
})
export class TrackFormComponent implements OnInit {
    private readonly restService = inject(RestService);
    private readonly routeService = inject(RouteService);
    private readonly listService = inject(ListService);
    track = input<TrackDto | null>(null);
    cancel = output();
    form: FormGroup;

    constructor(private fb: FormBuilder) {
        this.form = this.fb.group({
            id: [null],
            name: ["", Validators.required],
            shortName: ["", Validators.required],
            countryAlpha2: [null, Validators.required],
            city: ["", Validators.required]
        });
    }

    ngOnInit(): void {
        const track = this.track();
        if (track) {
            this.form.patchValue(track);
            return;
        }

        const trackId = this.routeService.getRouteParam("trackId");
        if (!trackId)
            return;

        this.restService.get<TrackDto>('/track/get-by-id/' + trackId).subscribe(res => {
            this.form.patchValue(res);
        });
    }

    saveAllChanges() {
        if (this.form.invalid)
            return;

        const form = this.form.value;
        if (form['id'])
            this.restService.post('/track/update', this.form.value).subscribe();
        else
            this.restService.post('/track/add', this.form.value).subscribe(() => this.onAddSuccess());
    }

    onCancel() {
        this.cancel.emit();
    }

    onAddSuccess() {
        this.listService.triggerReload();
        this.routeService.navigateToParent();
    }
}
