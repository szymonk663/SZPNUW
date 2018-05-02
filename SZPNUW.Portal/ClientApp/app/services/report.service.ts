import { Injectable } from '@angular/core';
import { Http, Headers, Response, URLSearchParams} from '@angular/http';
import 'rxjs/add/operator/map'

import {ReportModel} from '../viewmodels/ReportModel';


@Injectable()
export class ReportService {
    constructor(private http: Http) { }

    private url = 'Report/';

    getSectionReports(sectionId: number): Promise<ReportModel[]> {
        return this.http.get(this.url + 'GetSectionReports/' + sectionId)
            .toPromise()
            .then(result => {
                if (result.status == 200) {
                    return result.json();
                }
                return null;
            }).catch(this.handleError);
    }

    uploadReport(fileToUpload: any, sectionId: number): Promise<boolean> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('sectionId', sectionId.toString());
        let input = new FormData();
        input.append("file", fileToUpload);
        return this.http
            .post(this.url + 'upload', input, { search: params }).toPromise().then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    downloadReport(reportId: number) {
        window.open(this.url + 'DownloadReport/' + reportId);
    }

    deleteReport(reportId: number): Promise<boolean> {
        return this.http.delete(this.url + 'DeleteReport/' + reportId)
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    private handleError(error: Response): Promise<any> {
        return Promise.reject(error.json());
    }
}