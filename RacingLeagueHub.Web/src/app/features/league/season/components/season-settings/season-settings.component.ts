import { Component, computed, input } from '@angular/core';
import { SeasonAssistsDto, SeasonLobbySettingsDto } from '../../models/season.model';
import {
    CarDamageLabels, CarDamageRateLabels, CornerCuttingLabels, GearboxLabels, QualifyingTypeLabels,
    RaceDistanceLabels, RaceStartLabels, RacingLineLabels, SafetyCarLabels, TractionControlLabels, WeatherLabels
} from '../../models/season-settings.model';

interface SettingRow {
    label: string;
    value: string;
}

@Component({
    selector: 'season-settings',
    templateUrl: './season-settings.component.html'
})
export class SeasonSettingsComponent {
    assists = input<SeasonAssistsDto | null>(null);
    lobbySettings = input<SeasonLobbySettingsDto | null>(null);

    assistRows = computed<SettingRow[]>(() => {
        const a = this.assists();
        if (!a)
            return [];

        return [
            { label: 'Racing line', value: RacingLineLabels[a.racingLine] },
            { label: 'Gearbox', value: GearboxLabels[a.gearbox] },
            { label: 'Traction control', value: TractionControlLabels[a.tractionControl] },
            { label: 'ABS', value: onOff(a.abs) },
        ];
    });

    lobbyRows = computed<SettingRow[]>(() => {
        const l = this.lobbySettings();
        if (!l)
            return [];

        return [
            { label: 'Qualifying', value: QualifyingTypeLabels[l.qualifyingType] },
            { label: 'Race distance', value: RaceDistanceLabels[l.raceDistancePercentage] },
            { label: 'Formation lap', value: onOff(l.formationLap) },
            { label: 'Race start', value: RaceStartLabels[l.raceStart] },
            { label: 'Weather', value: WeatherLabels[l.weather] },
            { label: 'Safety car', value: SafetyCarLabels[l.safetyCar] },
            { label: 'Corner cutting', value: CornerCuttingLabels[l.cornerCutting] },
            { label: 'Car damage', value: CarDamageLabels[l.carDamage] },
            { label: 'Car damage rate', value: CarDamageRateLabels[l.carDamageRate] },
            { label: 'Collisions', value: onOff(l.collisions) },
            { label: 'Ghosting', value: onOff(l.ghosting) },
            { label: 'Parc fermé', value: onOff(l.parcFerme) },
            { label: 'Equal car performance', value: onOff(l.equalCarPerformance) },
        ];
    });
}

function onOff(value: boolean): string {
    return value ? 'On' : 'Off';
}
