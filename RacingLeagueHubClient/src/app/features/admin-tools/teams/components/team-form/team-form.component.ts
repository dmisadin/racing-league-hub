import { Component, inject, input, OnInit, output } from "@angular/core";
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { TeamDto } from "../../models/team.model";
import { RestService } from "../../../../../core/services/rest.service";
import { RouteService } from "../../../../../core/services/route.service";
import { InputTextComponent } from "../../../../../shared/components/input-fields/input-text/input-text.component";
import { ListService } from "../../../../../shared/services/list.service";
import { ToastService } from "../../../../../core/services/toast.service";

@Component({
    selector: 'team-form',
    imports: [ReactiveFormsModule, InputTextComponent],
    providers: [RouteService],
    templateUrl: './team-form.component.html',
})
export class TeamFormComponent implements OnInit {
    private readonly restService = inject(RestService);
    private readonly routeService = inject(RouteService);
    private readonly listService = inject(ListService);
    private readonly toastService = inject(ToastService);
    team = input<TeamDto | null>();
    cancel = output();
    form: FormGroup;

    constructor(private fb: FormBuilder) {
        this.form = this.fb.group({
            id: [null],
            name: ["", Validators.required],
            color: ["#000000"],
        });
    }

    ngOnInit(): void {
        const team = this.team();
        if (team) {
            this.form.patchValue(team);
            return;
        }

        const teamId = this.routeService.getRouteParam("teamId");
        if (!teamId)
            return;

        this.restService.get<TeamDto>(`/team/get-by-id/${teamId}`).subscribe(res => this.form.patchValue(res));
    }

    onSubmit() {
        if (this.form.invalid)
            return;

        const form = this.form.value;
        if (form['id'])
            this.restService.put(`/team/${form['id']}`, this.form.value).subscribe({
                next: () => this.toastService.showSuccess("Successfully updated the team."),
                error: () => this.toastService.showError("Failed to update the team.")
            });
        else
            this.restService.post('/team', this.form.value).subscribe(() => this.onAddSuccess());

    }

    onCancel() {
        this.cancel.emit();
    }

    onAddSuccess() {
        this.toastService.showSuccess("Successfully added a new team.");
        this.listService.triggerReload();
        this.routeService.navigateToParent();
    }

}
