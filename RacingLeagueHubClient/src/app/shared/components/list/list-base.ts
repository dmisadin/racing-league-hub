import { Directive, effect, inject, OnInit, signal } from "@angular/core";
import { RestService } from "../../../core/services/rest.service";
import { ListService } from "../../services/list.service";

@Directive()
export abstract class ListBase<T> {
    protected readonly restService = inject(RestService);
    protected readonly listService = inject(ListService);
    list = signal<T[] | null>(null);

    protected abstract dtoEndpoint: string;

    constructor() {
        effect(() => {
            this.listService.refresh();
            this.loadList();
        })
    }

    protected loadList() {
        this.restService.get<T[]>(`/${this.dtoEndpoint}/get-all`).subscribe(res => this.list.set(res));
    }
}