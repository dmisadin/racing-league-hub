import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Platform } from 'app/shared/models/platform'
import { Observable } from 'rxjs';
import { AbstractGetService } from './abstractGetService';
@Injectable({
    providedIn: 'root'
})
export class PlatformListService extends AbstractGetService<Platform> {
    constructor(http: HttpClient) {
        super(http, 'https://localhost:44347/api/Platform');
    }
}
