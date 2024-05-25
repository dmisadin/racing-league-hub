import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ControlContainer, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GameListService } from 'app/core/services/game-list.service';
import { PlatformListService } from 'app/core/services/platform-list.service';
import { Platform, Game } from 'app/shared/models/enums/Enumerations';

@Component({
    selector: 'app-season-info-form',
    templateUrl: './season-info-form.component.html',
    styleUrls: ['./season-info-form.component.scss'],
})


export class SeasonInfoFormComponent implements OnInit {
    @Output() infoValueChange = new EventEmitter();

    platformEnum = Platform;
    gameEnum = Game;
    defaultLapsRequiredPercentage = 90;

    constructor(private fb: FormBuilder,
        private parentControl: ControlContainer,
        private gameListService: GameListService,
        private platformListService: PlatformListService,
    ) { }

    parentFormGroup = this.parentControl.control as FormGroup;

    infoFormGroup = this.fb.group({
        name: ["", Validators.required],
        game: [this.gameEnum.F124.Value, Validators.required],
        platform: [this.platformEnum.Steam.Value, Validators.required],
        lapsRequiredPercentage: [this.defaultLapsRequiredPercentage, [Validators.required, Validators.min(0), Validators.max(100)]],
    });

    ngOnInit(): void {
        this.parentFormGroup.addControl('seasonInfo', this.infoFormGroup);
    }

    get nameField(): FormControl {
        return this.infoFormGroup?.get('name') as FormControl;
    }

    get lapsField(): FormControl {
        return this.infoFormGroup?.get('lapsRequiredPercentage') as FormControl;
    }

    sortOrder = () => { return 0 };
}