import { Directive, effect, inject, signal } from "@angular/core";
import { RestService } from "../../../core/services/rest.service";
import { ListService } from "../../services/list.service";
import { PagedResult } from "../../models/models";

@Directive()
export abstract class ListBase<T> {
    protected readonly restService = inject(RestService);
    protected readonly listService = inject(ListService);

    list = signal<T[] | null>(null);
    currentPage = signal(1);
    hasMore = signal(false);
    isLoading = signal(false);

    protected abstract dtoEndpoint: string;

    constructor() {
        effect(() => {
            this.listService.refresh();
            this.currentPage.set(1);
            this.loadPage();
        })
    }

    loadMore(): void {
        this.currentPage.update(page => page++);
        this.loadPage();
    }
    
    private loadPage(): void {
        this.isLoading.set(true);
        this.getApiCall(this.dtoEndpoint).subscribe({
            next: (result) => {
                this.list.update(current => [...(current ?? []), ...result.items]);
                this.hasMore.set(result.hasMore);
                this.isLoading.set(false);
            },
            error: () => this.isLoading.set(false)
        });
    }

    protected getApiCall (endpoint: string) {
        return this.restService.get<PagedResult<T>>(endpoint, { page: this.currentPage() });
    }
}