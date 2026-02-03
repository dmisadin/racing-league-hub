import { Observable } from "rxjs";
import { HttpClient } from '@angular/common/http';

export abstract class AbstractGetService<T> {
    constructor(protected http: HttpClient, protected actionUrl: string) {
    }

    getAll(): Observable<T[]> {
        return this.http.get(this.actionUrl) as Observable<T[]>;
    }

    getOne(id: number): Observable<T> {
        return this.http.get(`${this.actionUrl}${id}`) as Observable<T>;
    }
} 