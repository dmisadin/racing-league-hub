import { Directive, effect, inject, signal } from "@angular/core";
import { RestService } from "../../../core/services/rest.service";
import { ListService } from "../../services/list.service";

@Directive()
export abstract class ModalFormParent<TDto> {
    protected readonly restService = inject(RestService);
    protected readonly listService = inject(ListService);
    protected dto = signal<TDto | null>(null);

    constructor() {
        effect(() => {
            this.listService.refresh();
            this.loadDto();
        })
    }

    protected abstract loadDto(): void;
}