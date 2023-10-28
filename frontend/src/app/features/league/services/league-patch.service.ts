import { Injectable } from '@angular/core';
import { AbstractPatchService } from '../../../core/services/abstractPatchService';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class LeaguePatchService extends AbstractPatchService<any> {

    constructor(http: HttpClient) {
        super(http, 'https://localhost:44347/api/League/');
    }
}
