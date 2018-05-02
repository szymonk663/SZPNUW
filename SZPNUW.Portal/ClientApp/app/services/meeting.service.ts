import { Injectable } from '@angular/core';
import { Http, Headers, Response, URLSearchParams} from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

import {MeetingModel} from '../viewmodels/MeetingModel';


@Injectable()
export class MeetingService {
    constructor(private http: Http) { }

    private url = 'Meeting/';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    getMeetingsBySectionStudent(sectionId: number, studentId: number): Promise<MeetingModel[]> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('sectionId', sectionId.toString());
        params.set('studentId', studentId.toString());
        return this.http.get(this.url, { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    update(meeting: MeetingModel): Promise<boolean> {
        return this.http.put(this.url + 'UpdateMeeting', JSON.stringify(meeting), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    AddMeeting(meeting: MeetingModel): Promise<boolean> {
        return this.http.post(this.url + 'AddMeeting', JSON.stringify(meeting), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    delete(id: number): Promise<boolean> {
        return this.http.delete(this.url + 'DeleteMeeting/' + id)
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