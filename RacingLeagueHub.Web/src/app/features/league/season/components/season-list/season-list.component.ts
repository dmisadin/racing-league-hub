import { Component, input, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { map, tap } from 'rxjs';
import { ListBase } from '../../../../../shared/components/list/list-base';
import { GameLabelPipe } from '../../../../../shared/pipes/game-label.pipe';
import { LeagueSeasonsOverviewDto, SeasonStatus, SeasonSummaryDto } from '../../models/season-summary.model';
import { CurrentSeasonCardComponent } from '../current-season-card/current-season-card.component';
import { SeasonSummaryCardComponent } from '../season-summary-card/season-summary-card.component';

/** `list` holds the paged completed seasons; current and upcoming arrive in full with the first page. */
@Component({
    selector: 'season-list',
    imports: [RouterLink, GameLabelPipe, CurrentSeasonCardComponent, SeasonSummaryCardComponent],
    templateUrl: './season-list.component.html'
})
export class SeasonListComponent extends ListBase<SeasonSummaryDto> {
    leagueSlug = input.required<string>();
    canEdit = input(false);

    overview = signal<LeagueSeasonsOverviewDto | null>(null);

    protected override get endpoint(): string {
        return `/leagues/${this.leagueSlug()}/seasons/summaries`;
    }

    protected override getApiCall() {
        if (this.currentPage() > 1)
            return this.restService.get<LeagueSeasonsOverviewDto['completedSeasons']>(
                this.endpoint,
                { status: SeasonStatus.Completed, page: this.currentPage() });

        this.overview.set(null);
        return this.restService
            .get<LeagueSeasonsOverviewDto>(`/leagues/${this.leagueSlug()}/seasons/overview`)
            .pipe(
                tap(overview => this.overview.set(overview)),
                map(overview => overview.completedSeasons));
    }
}
