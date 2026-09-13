import { Injectable, signal } from "@angular/core";


@Injectable()
export class ListService {
    private listChanged = signal<number>(0);

    public refresh = this.listChanged.asReadonly();

    public triggerReload() {
        this.listChanged.set(Date.now());
    }
}