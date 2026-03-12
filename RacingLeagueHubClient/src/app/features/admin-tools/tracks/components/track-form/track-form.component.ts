import { Component, inject, output } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { CountryPickerComponent } from "../../../../../shared/components/input-fields/country-picker/country-picker.component";
import { TrackDto } from "../../models/track.model";
import { InputNumberComponent } from "../../../../../shared/components/input-fields/input-number/input-number.component";
import { InputTextComponent } from "../../../../../shared/components/input-fields/input-text/input-text.component";
import { RestService } from "../../../../../core/services/rest.service";

@Component({
    selector: 'track-form',
    imports: [ReactiveFormsModule, CountryPickerComponent, InputNumberComponent, InputTextComponent],
    templateUrl: './track-form.component.html',
})
export class TrackFormComponent {
    restService = inject(RestService);
    cancel = output();
    form: FormGroup;

    constructor(private fb: FormBuilder) {
        this.form = this.fb.group({
            name: ["", Validators.required],
            shortName: ["", Validators.required],
            country: [null, Validators.required],
            city: ["", Validators.required],
            elevation: [0]
        });
    }

    saveAllChanges() {
        if (this.form.invalid)
            return;

        const form = this.form.value;
        const cleanForm: TrackDto = { ...form, countryAlpha2: form['country'].alpha2 }

        this.restService.post('/track/add', cleanForm).subscribe();
    }

    onCancel() {
        this.cancel.emit();
    }
}
