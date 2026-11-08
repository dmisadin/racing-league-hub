import { Component, computed, input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { getSeasonStatus, SeasonStatus, SeasonSummaryDto } from '../../models/season-summary.model';
import { GameLabelPipe } from '../../../../../shared/pipes/game-label.pipe';

@Component({
    selector: 'season-summary-card',
    imports: [DatePipe, RouterLink, GameLabelPipe],
    templateUrl: './season-summary-card.component.html'
})
export class SeasonSummaryCardComponent {
    summary = input.required<SeasonSummaryDto>();

    status = computed(() => getSeasonStatus(this.summary()));

    readonly SeasonStatus = SeasonStatus;
}
