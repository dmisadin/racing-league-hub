import { Component, inject, input, OnInit } from "@angular/core";
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

@Component({
    selector: 'track-layout-form',
    imports: [ReactiveFormsModule, InputNumberComponent, InputTextComponent, NgSelectComponent],
    templateUrl: './track-layout-form.component.html',
})
export class TrackLayoutFormComponent implements OnInit {
    private readonly fb = inject(FormBuilder);
    private readonly restService = inject(RestService);

    trackLayoutId = input<number>();
    trackId = input.required<string>();
    trackLayout = input<TrackLayoutDto | null>(null);

    form!: FormGroup;
    gameChoices: DropdownOption[] = enumToOptions(Game);

    ngOnInit(): void {
        const layout = this.trackLayout();

        if (layout) {
            this.buildForm(layout);
            return;
        }

        this.restService.get<TrackLayoutDto>('/track-layout/get-by-id/' + this.trackLayoutId())
            .subscribe(res => this.buildForm(res))
    }

    private buildForm(layout: TrackLayoutDto | null) {
        this.form = this.fb.group({
            id: [layout?.id],
            trackId: [layout?.trackId ?? this.trackId(), Validators.required],
            name: [layout?.name ?? "", Validators.required],
            pitStopDuration: [layout?.pitStopDuration],
            cornersTotal: [layout?.cornersTotal],
            cornersLeft: [layout?.cornersLeft, [Validators.min(0), maxFromControl('cornersTotal')]],
            lapsGrandPrix: [layout?.lapsGrandPrix],
            trackLayoutGames: [layout?.trackLayoutGames ?? []]
        });
    }

    saveAllChanges() {
        if (this.form.invalid)
            return;

        //this.restService.post('/track/add', cleanForm).subscribe();
    }

    onCancel() {
        //reset form
    }
}
