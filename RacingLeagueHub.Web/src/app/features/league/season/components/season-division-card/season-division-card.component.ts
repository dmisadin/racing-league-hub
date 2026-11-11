import { Component, input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { SeasonDivisionSummaryDto } from '../../models/season.model';
import { SeasonStatus } from '../../models/season-summary.model';

@Component({
    selector: 'season-division-card',
    imports: [DatePipe, RouterLink],
    templateUrl: './season-division-card.component.html'
})
export class SeasonDivisionCardComponent {
    summary = input.required<SeasonDivisionSummaryDto>();
    link = input.required<string[]>();

    readonly SeasonStatus = SeasonStatus;
}
