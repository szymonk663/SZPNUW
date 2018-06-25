import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

import { SemesterModel } from '../viewmodels/SemesterModel'
import { Result } from '../viewmodels/Result';

@Injectable()
export class SemesterService {
    constructor(private http: Http) { }

    private url = 'Semester/';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    getSemesters(): Promise<SemesterModel[]> {

        return this.http.get(this.url + 'GetSemesters').toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSemestersBySubjectId(id: number): Promise<SemesterModel[]> {

        return this.http.get(this.url + 'GetSemestersBySubjectId/' + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSemestersByStudentId(id: number): Promise<SemesterModel[]> {

        return this.http.get(this.url + 'GetSemestersBySubjectId/' + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSemester(id: number) {
        return this.http.get(this.url + "GetSemesterById/" + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as SemesterModel;
            }
            return null;
        }).catch(this.handleError);
    }

    addSemester(semester: SemesterModel): Promise<Result> {
        return this.http.post(this.url + 'AddSemester', JSON.stringify(semester), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    updateSemester(semester: SemesterModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateSemester', JSON.stringify(semester), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    getSemestersById(id: number): Promise<SemesterModel> {
        return this.http.get(this.url + id).toPromise().then(result => {
            if (result.status == 200) {
                return <SemesterModel>result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getDepartments(): Promise<string[]> {
        return this.http.get(this.url + 'GetDepartments').toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getYears(): Promise<string[]> {
        return this.http.get(this.url + 'GetYears').toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSemestersNum(): Promise<number[]> {
        return this.http.get(this.url + 'GetSemestersNum').toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    private handleError(error: Response): Promise<any> {
        return Promise.reject(error.json());
    }
}