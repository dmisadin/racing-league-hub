import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faPlay } from '@fortawesome/free-solid-svg-icons';
import { GrandprixDataService } from 'app/core/services/grandprix-data.service';
import { GrandPrix } from 'app/shared/models/grandprix/GrandPrix';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-grandprix',
    templateUrl: './grandprix.component.html',
    styleUrls: ['./grandprix.component.scss'],
})
export class GrandPrixComponent {
    faPlay = faPlay;
    gpItem$!: Subscription;
    gpItem = new GrandPrix();
    gpId: number = 0;
    constructor(private gpDataService: GrandprixDataService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.gpItem$ = this.route.params.subscribe(params => {
            this.gpId = params['id'];
        })
        if (this.gpId) {
            this.gpDataService.getOne(this.gpId).subscribe((data) => {
                console.log(data)
            })
        }
    }
}
