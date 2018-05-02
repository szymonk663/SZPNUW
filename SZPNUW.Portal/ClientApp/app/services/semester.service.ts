import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

import { SemesterModel } from '../viewmodels/SemesterModel'

@Injectable()
export class SemesterService {
    constructor(private http: Http) { }

    private url = 'Semesters/';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    getSemesters(): Promise<SemesterModel[]> {

        return this.http.get(this.url + 'semesters').toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSemestersBySubjectId(id: number): Promise<SemesterModel[]> {

        return this.http.get(this.url + 'semesters/' + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSemestersByStudentId(id: number): Promise<SemesterModel[]> {

        return this.http.get(this.url + 'semesters/student/' + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSemester(id: number) {
        return this.http.get(this.url + id).toPromise().then(result => {
            if (result.status == 200) {
                return <SemesterModel>result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    new(semester: SemesterModel): Promise<boolean> {
        return this.http.post(this.url + 'new', JSON.stringify(semester), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    update(semester: SemesterModel): Promise<boolean> {
        return this.http.put(this.url + 'update', JSON.stringify(semester), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
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
        return this.http.get(this.url + 'departments').toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getYears(): Promise<string[]> {
        return this.http.get(this.url + 'years').toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSemestersNum(): Promise<number[]> {
        return this.http.get(this.url + 'semestersnum').toPromise().then(result => {
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