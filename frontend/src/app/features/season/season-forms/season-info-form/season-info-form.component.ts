import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ControlContainer, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GameListService } from 'app/core/services/game-list.service';
import { Game } from 'app/shared/models/game';
import { PlatformListService } from 'app/core/services/platform-list.service';
import { Platform } from 'app/shared/models/platform'

@Component({
    selector: 'app-season-info-form',
    templateUrl: './season-info-form.component.html',
    styleUrls: ['./season-info-form.component.scss'],
})


export class SeasonInfoFormComponent implements OnInit {
    @Input() infoValue = { name: "Sezona 5", gameId: 5, platformId: 1, lapsRequiredPercentage: 90 };
    @Output() infoValueChange = new EventEmitter();
    gameList: Game[] = [];
    platformList: Platform[] = [];

    constructor( private fb: FormBuilder, 
        private parentControl: ControlContainer, 
        private gameListService: GameListService,
        private platformListService: PlatformListService,
    ) { }

    parentFormGroup = this.parentControl.control as FormGroup;

    infoFormGroup = this.fb.group({
        name: [this.infoValue.name, Validators.required],
        gameId: [this.infoValue.gameId, Validators.required],
        platformId: [this.infoValue.platformId, Validators.required],
        lapsRequiredPercentage: [this.infoValue.lapsRequiredPercentage, [Validators.required, Validators.min(0), Validators.max(100)]],
    });

    ngOnInit(): void {
        console.log("infovalue on init", this.infoValue);
        this.parentFormGroup.addControl('info', this.infoFormGroup);

        this.gameListService.getAll().subscribe((data) => {
            this.gameList = data;
            console.log(this.gameList);
        })

        this.platformListService.getAll().subscribe((data) => {
            this.platformList = data;
            console.log(this.platformList);
        })
    }

    get nameField(): FormControl {
        return this.infoFormGroup?.get('name') as FormControl;
    }

    get lapsField(): FormControl {
        return this.infoFormGroup?.get('lapsRequiredPercentage') as FormControl;
    }
}