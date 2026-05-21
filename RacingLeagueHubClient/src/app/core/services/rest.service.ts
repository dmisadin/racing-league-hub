import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class RestService {
    private httpClient = inject(HttpClient);

    get<T>(path: string, queryParams?: Record<string, string | number | boolean>) {
        const params = queryParams
            ? Object.entries(queryParams).reduce(
                (p, [key, value]) => p.set(key, value.toString()),
                new HttpParams()
            )
            : undefined;

        return this.httpClient.get<T>(environment.apiUrl + path, { params });
    }

    getFile<T>(path: string) {
        return this.httpClient.get(environment.apiUrl + path, { responseType: 'blob' });
    }

    put<T>(path: string, data?: any) {
        return this.httpClient.put<T>(environment.apiUrl + path, data);
    }
    
    post<T>(path: string, data?: any) {
        return this.httpClient.post<T>(environment.apiUrl + path, data);
    }

    save(path: string, data?: any) {
        return this.httpClient.post(environment.apiUrl + path, data, { responseType: 'text' });
    }

    delete(path: string) {
        return this.httpClient.delete(environment.apiUrl + path);
    }
}
