import { Injectable } from '@angular/core';
import { Http, Headers, Response, URLSearchParams} from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

import {SubjectModel} from '../viewmodels/SubjectModel';
import {SubjectSemesterModel} from '../viewmodels/SubjectSemesterModel';
import { Result } from '../viewmodels/Result';

@Injectable()
export class SubjectService {
    constructor(private http: Http) { }

    private url = 'Subject/';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    getSubjects(department: string, semesterNum: number): Promise<SubjectModel[]> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('department', department);
        params.set('semesterNumber', semesterNum.toString());
        return this.http.get(this.url + "GetSubjects", { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }
    getSubjectsBySemester(semesterId: number): Promise<SubjectModel[]> {
        return this.http.get(this.url + 'GetSubjectsBySemesterId/' + semesterId).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSubject(id: number): Promise<SubjectModel> {
        return this.http.get(this.url + "GetSubjectById/" + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as SubjectModel;
            }
            return null;
        }).catch(this.handleError);
    }

    updateSubject(subject: SubjectModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateSubject', JSON.stringify(subject), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    addSubject(subject: SubjectModel): Promise<Result> {
        return this.http.post(this.url + 'AddSubject', JSON.stringify(subject), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    getSubjectSemester(id_subject: number, id_semester: number): Promise<SubjectSemesterModel> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('subjectId', id_subject.toString());
        params.set('semesterId', id_semester.toString());
        return this.http.get(this.url + 'GetSubjectSemester', { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as SubjectSemesterModel;
            }
            return null;
        }).catch(this.handleError);
    }

    updateSemester(subSem: SubjectSemesterModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateSubjectSemester', JSON.stringify(subSem), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    addSemester(subSem: SubjectSemesterModel): Promise<Result> {
        return this.http.post(this.url + 'AddSubjectSemester', JSON.stringify(subSem), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    deleteSemester(id: number): Promise<Result> {
        return this.http.delete(this.url + 'DeleteSubjectSemester/' + id)
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    private handleError(error: Response): Promise<any> {
        return Promise.reject(error.text());
    }
}