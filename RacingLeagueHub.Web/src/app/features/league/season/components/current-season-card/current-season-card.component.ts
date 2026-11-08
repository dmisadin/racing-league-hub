import { Component, computed, input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { getSeasonProgress, getSeasonStatus, SeasonStatus, SeasonSummaryDto } from '../../models/season-summary.model';
import { Platform } from '../../models/season.model';
import { GameLabelPipe } from '../../../../../shared/pipes/game-label.pipe';

@Component({
    selector: 'current-season-card',
    imports: [DatePipe, RouterLink, GameLabelPipe],
    templateUrl: './current-season-card.component.html'
})
export class CurrentSeasonCardComponent {
    summary = input.required<SeasonSummaryDto>();

    status = computed(() => getSeasonStatus(this.summary()));
    progress = computed(() => getSeasonProgress(this.summary()));

    readonly SeasonStatus = SeasonStatus;
    readonly Platform = Platform;
}
