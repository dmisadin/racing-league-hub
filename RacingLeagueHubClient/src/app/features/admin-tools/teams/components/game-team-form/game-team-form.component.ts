import { Component, inject, input, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RestService } from '../../../../../core/services/rest.service';
import { RouteService } from '../../../../../core/services/route.service';
import { Game } from '../../../../../shared/models/enums';
import { DropdownOption } from '../../../../../shared/models/models';
import { enumToOptions } from '../../../../../shared/utilities/enum.utility';
import { GameTeamDto } from '../../models/team.model';
import { NgSelectComponent } from "@ng-select/ng-select";
import { InputNumberComponent } from "../../../../../shared/components/input-fields/input-number/input-number.component";
import { InputTextComponent } from "../../../../../shared/components/input-fields/input-text/input-text.component";
import { ListService } from '../../../../../shared/services/list.service';
import { InputFileComponent } from "../../../../../shared/components/input-fields/input-file/input-file.component";

@Component({
    selector: 'game-team-form',
    imports: [ReactiveFormsModule, NgSelectComponent, InputNumberComponent, InputTextComponent, InputFileComponent],
    providers: [RouteService],
    templateUrl: './game-team-form.component.html',
})
export class GameTeamFormComponent {
    private readonly fb = inject(FormBuilder);
    private readonly routeService = inject(RouteService);
    private readonly restService = inject(RestService);
    private readonly listService = inject(ListService);

    gameTeamId = input<string>();
    gameTeam = input<GameTeamDto | null>(null);
    cancel = output();

    form: FormGroup;
    gameChoices: DropdownOption[] = enumToOptions(Game);

    constructor() {
        const teamId = this.routeService.getRouteParam("teamId");
        
        this.form = this.fb.group({
            id: [null],
            game: [Game.F125, Validators.required],
            teamId: [teamId, Validators.required],
            name: ["", Validators.required],
            shortName: ["", Validators.required],
            abbreviation: ["", [Validators.required, Validators.maxLength(3)]],
            color: ["#000000", Validators.required],
            telemetryId: [],
            logoResourceId: ["", Validators.required]
        });
    }

    ngOnInit(): void {
        const gameTeam = this.gameTeam();

        if (gameTeam) {
            this.form.patchValue(gameTeam);
        } else if (this.gameTeamId()) {
            this.loadGameTeam();
        }
    }

    private loadGameTeam() {
        this.restService.get<GameTeamDto>('/game-team/get-by-id/' + this.gameTeamId())
            .subscribe(gameTeam => {
                this.form.patchValue(gameTeam);
            });
    }

    saveAllChanges() {
        if (this.form.invalid)
            return;
        
        const form = this.form.value;
        if (form['id'])
            this.restService.post('/game-team/update', this.form.value).subscribe();
        else
            this.restService.post('/game-team/add', this.form.value).subscribe(() => this.onAddSuccess());
    }

    onCancel() {
        this.cancel.emit();
    }

    onAddSuccess() {
        this.listService.triggerReload();
        this.routeService.navigateToParent();
    }
}
