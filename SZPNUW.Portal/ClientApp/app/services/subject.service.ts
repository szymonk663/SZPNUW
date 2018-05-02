import { Injectable } from '@angular/core';
import { Http, Headers, Response, URLSearchParams} from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

import {SubjectModel} from '../viewmodels/SubjectModel';
import {SubjectSemesterModel} from '../viewmodels/SubjectSemesterModel';

@Injectable()
export class SubjectService {
    constructor(private http: Http) { }

    private url = 'Subject/';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    getSubjects(department: string, semesterNum: number): Promise<SubjectModel[]> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('department', department);
        params.set('semesterNum', semesterNum.toString());
        return this.http.get(this.url, { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }
    getSubjectsBySemester(semesterId: number): Promise<SubjectModel[]> {
        return this.http.get(this.url + 'GetSubjectsBySemester/' + semesterId).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSubject(id: number): Promise<SubjectModel> {
        return this.http.get(this.url + id).toPromise().then(result => {
            if (result.status == 200) {
                return <SubjectModel>result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    update(subject: SubjectModel): Promise<boolean> {
        return this.http.put(this.url + 'UpdateSubject', JSON.stringify(subject), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    add(subject: SubjectModel): Promise<boolean> {
        return this.http.post(this.url + 'AddSubject', JSON.stringify(subject), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    getSubjectSemester(id_subject: number, id_semester: number): Promise<SubjectSemesterModel> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('idsubject', id_subject.toString());
        params.set('idsemester', id_semester.toString());
        return this.http.get(this.url + 'GetSubjectSemester', { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return <SubjectSemesterModel>result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    updateSemester(subSem: SubjectSemesterModel): Promise<boolean> {
        return this.http.put(this.url + 'UpdateSemester', JSON.stringify(subSem), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    addSemester(subSem: SubjectSemesterModel): Promise<boolean> {
        return this.http.post(this.url + 'AddSemester', JSON.stringify(subSem), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    deleteSemester(id: number): Promise<boolean> {
        return this.http.delete(this.url + 'DeleteSemester/' + id)
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