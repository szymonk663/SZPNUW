import { Injectable } from '@angular/core';
import { Http, Headers, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import { SysLogModel } from '../viewmodels/SysLogModel';

@Injectable()
export class SysLogService {
    constructor(private http: Http) { }

    private url = 'SysLog/';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    private handleError(error: Response): Promise<any> {
        if(error)
            return Promise.reject(error.text());
        return Promise.reject(null);
    }

    getSysLogs(): Promise<SysLogModel[]> {
        return this.http.get(this.url + 'GetSysLogs').toPromise().then(result => {
            if (result.status == 200) {
                console.log(result.json());
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }
}