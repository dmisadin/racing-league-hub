import { Injectable } from "@angular/core";
import { IApiBaseActions, ParamsType } from "../../shared/interfaces/api-base-actions.interface";
import { HttpClient, HttpParams } from "@angular/common/http";
import { HttpStatusCode } from "@angular/common/http";
import { tap } from "rxjs/internal/operators/tap";

@Injectable({
    providedIn: 'root'
})
export class ApiHandlerService implements IApiBaseActions {
    constructor(public httpClient: HttpClient) {
    }

    Get(url: string, params?: ParamsType) {
        return this.httpClient
            .get(url, { params: this.createParams(params) })
            .pipe(tap((x) => this.HandleResponse(x)));
    }

    GetAll(url: string, params?: ParamsType) {
        return this.httpClient
            .get(url, { params: this.createParams(params) })
            .pipe(tap((x) => this.HandleResponse(x)));
    }

    Post(url: string, data: any, params?: ParamsType) {
        return this.httpClient
            .post(url, data, { params: this.createParams(params) })
            .pipe(tap((x) => this.HandleResponse(x)));
    }

    Delete(url: string, data: any, params?: ParamsType) {
        return this.httpClient
            .delete(url, { params: this.createParams(params) })
            .pipe(tap((x) => this.HandleResponse(x)));
    }

    Put(url: string, data: any, params?: ParamsType) {
        return this.httpClient
            .put(url, data, { params: this.createParams(params) })
            .pipe(tap((x) => this.HandleResponse(x)));
    }

    HandleResponse(response: any) {
        if (response.Status === 500) {
            alert(HttpStatusCode.InternalServerError);
        }
    }

    createParams(params?: ParamsType) {
        let httpParams = new HttpParams();
        if (params) {
            Object.entries(params).forEach(([key, value]) => {
                httpParams = httpParams.append(key, value);
            });
        }
        return httpParams;
    }
}