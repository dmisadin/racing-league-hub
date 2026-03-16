import { inject, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Injectable()
export class RouteService {
    private readonly router = inject(Router);
    private readonly activatedRoute = inject(ActivatedRoute);
    private readonly location = inject(Location);

    public navigateTest(): void {
        this.router.navigate(["../../../"], { relativeTo: this.activatedRoute })
    }

    public navigateToParent(): void {
        this.router.navigate(["../"], { relativeTo: this.activatedRoute })
    }

    public navigateToGrandParent(): void {
        this.router.navigate(["../../"], { relativeTo: this.activatedRoute })
    }

    public navigateToRelative(...pathFragments: string[]): void {
        this.router.navigate(pathFragments, { relativeTo: this.activatedRoute })
    }

    public getId(isNullable = false): string {
        let selector = 'id';

        if (isNullable)
            selector = 'id?';

        return this.activatedRoute.snapshot.params[selector];
    }

    public getParentId(isNullable = false): string {
        let selector = 'id';

        if (isNullable)
            selector = 'id?';

        return this.activatedRoute.parent?.snapshot.params[selector];
    }

    public goBack() {
        this.location.back();
    }

    public getRouteParam(dataProperty: string): string | null {
        return this.activatedRoute.snapshot.params[dataProperty] || null;
    }
}
