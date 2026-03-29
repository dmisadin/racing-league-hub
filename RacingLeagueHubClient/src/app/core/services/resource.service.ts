import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ResourceDto } from '../../shared/models/resource';

@Injectable({ providedIn: 'root' })
export class ResourceService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/resource`;

    getById(id: string): Observable<ResourceDto> {
        return this.http.get<ResourceDto>(`${this.apiUrl}/get-by-id/${id}`);
    }

    getAll(): Observable<ResourceDto[]> {
        return this.http.get<ResourceDto[]>(`${this.apiUrl}/get-all`);
    }

    upload(file: File, isThumbnail?: boolean): Observable<ResourceDto> {
        const formData = new FormData();
        formData.append('file', file);

        if (isThumbnail != null)
            formData.append('isThumbnail', String(isThumbnail));

        return this.http.post<ResourceDto>(`${this.apiUrl}/upload`, formData);
    }

    delete(uid: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/delete/${uid}`);
    }
}
