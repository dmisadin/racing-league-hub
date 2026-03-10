import { Component, inject, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { GameTeamDto, GameTeamFormModel, TeamDto } from "../../models/team.model";
import { Game } from "../../../../../shared/models/enums";
import { RestService } from "../../../../../core/services/rest.service";

@Component({
    selector: 'team-form',
    imports: [ReactiveFormsModule],
    template: ``,
})
export class TeamFormComponent implements OnInit {
    private restService = inject(RestService);

    formGroup: FormGroup;
    gameTeamsForm: FormArray;

    constructor(private fb: FormBuilder) {
        this.formGroup = this.fb.group({
            name: ["", Validators.required],
            color: [""],
        });

        this.gameTeamsForm = this.fb.array([]);
    }

    ngOnInit(): void {
        // get id from route
        // this.restService.get<TeamDto>('/team/get-by-id?id=').subscribe(res => console.log(res));
    }

    createGameTeam(team?: Partial<GameTeamFormModel>): FormGroup {
        return this.fb.group({
            id: [team?.id ?? 0, Validators.required],
            game: [team?.game ?? Game.F125, Validators.required],
            color: [team?.color ?? '#000'],
            removable: [team?.removable ?? true]
        });
    }

    addGameTeam() {
        this.gameTeamsForm.push(this.createGameTeam());
    }

    removeGameTeam(index: number) {
        this.gameTeamsForm.removeAt(index);
    }

    loadGameTeams(dtos: GameTeamDto[]) {
        this.gameTeamsForm.clear();

        dtos.forEach(dto => {
            this.gameTeamsForm.push(this.createGameTeam({...dto, removable: false}));
        });
    }
}