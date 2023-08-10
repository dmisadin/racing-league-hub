import { Observable } from "rxjs";
import { HttpClient } from '@angular/common/http';

export abstract class AbstractPatchService<T> {
    constructor(protected _http: HttpClient, protected actionUrl: string) {
    }

    patch(id: number, data: any[]): Observable<T> {
        return this._http.patch(`${this.actionUrl}${id}`, data) as Observable<T>;
    }
} 