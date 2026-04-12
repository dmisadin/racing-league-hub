import { inject, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Injectable()
export class RouteService {
    private readonly router = inject(Router);
    private readonly activatedRoute = inject(ActivatedRoute);
    private readonly location = inject(Location);

    public navigateToNotFoundPage(): void {
        this.router.navigate(["/not-found"]);
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

    public getRouteParam(paramName: string): string | null {
        return this.activatedRoute.snapshot.pathFromRoot
                    .map(r => r.params[paramName])
                    .find(Boolean);
    }

    public getCurrentRouteParam(paramName: string): string | null {
        return this.activatedRoute.snapshot.params[paramName] || null;
    }

    public getParentRouteParam(paramName: string): string | null {
        return this.activatedRoute.parent?.snapshot.params[paramName] || null;
    }

}
