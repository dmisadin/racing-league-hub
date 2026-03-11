import { Component, inject } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { ModalComponent } from "../../../../../shared/components/modal/modal.component";
import { ActivatedRoute, Router } from "@angular/router";
import { CountryPickerComponent } from "../../../../../shared/components/input-fields/country-picker/country-picker.component";
import { CountryLean } from "../../../../../shared/models/country";
import { TrackDto } from "../../models/track.model";
import { InputNumberComponent } from "../../../../../shared/components/input-fields/input-number/input-number.component";
import { InputTextComponent } from "../../../../../shared/components/input-fields/input-text/input-text.component";

@Component({
    selector: 'track-form',
    imports: [ReactiveFormsModule, ModalComponent, CountryPickerComponent, InputNumberComponent, InputTextComponent],
    templateUrl: './track-form.component.html',
})
export class TrackFormComponent {
    router = inject(Router);
    route = inject(ActivatedRoute);
    form: FormGroup;

    constructor(private fb: FormBuilder) {
        this.form = this.fb.group({
            name: ["", Validators.required],
            shortName: ["", Validators.required],
            country: [null, Validators.required],
            city: ["", Validators.required],
            elevation: [0],
            test: [0, [Validators.required, Validators.min(2)]]
        });
    }

    onModalClosed() {
        this.router.navigate(['../'], { relativeTo: this.route });
    }

    onModalDiscarded() {
        this.onModalClosed();
    }

    saveAllChanges() {
        console.log("errors", this.form.controls['test'])
        if (this.form.invalid)
            return;

        const form = this.form.value;
        const cleanForm: TrackDto = { ...form, country: form['country'].alpha2 }
        console.log("form value", this.form.valid, this.form.value, cleanForm)
    }

    onCountrySelected(event: CountryLean) {
        console.log("forma", event);
    }
}