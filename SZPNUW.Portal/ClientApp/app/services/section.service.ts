import { Injectable } from '@angular/core';
import { Http, Headers, Response, URLSearchParams} from '@angular/http';
import 'rxjs/add/operator/map'

import {SectionStudentsModel} from '../viewmodels/SectionStudentsModel';
import {SectionsCreateModel} from '../viewmodels/SectionsCreateModel';
import {StudentSectionModel} from '../viewmodels/StudentSectionModel';
import {SectionModel} from '../viewmodels/SectionModel';

@Injectable()
export class SectionService {
    constructor(private http: Http) { }

    private url = 'Section/';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    getSectionsStudents(subjectId: number, semesterId: number): Promise<SectionStudentsModel[]> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('subjectId', subjectId.toString());
        params.set('semesterId', semesterId.toString());
        return this.http.get(this.url + 'GetSectionsStudents', { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getSectionStudentId(sectionId: number, studentId: number): Promise<number> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('sectionId', sectionId.toString());
        params.set('studentId', studentId.toString());
        return this.http.get(this.url + 'GetSectionStudentId', { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    studentInSection(sectionId: number, studentId: number): Promise<boolean> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('sectionId', sectionId.toString());
        params.set('studentId', studentId.toString());
        return this.http.get(this.url + 'StudentInSection', { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    addNewSections(sectionsCreate: SectionsCreateModel): Promise<string> {
        return this.http.post(this.url + 'AddSections', JSON.stringify(sectionsCreate), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return JSON.stringify(result.json());
                return null;
            }).catch(this.handleError);
    }

    update(section: SectionModel): Promise<boolean> {
        return this.http.put(this.url + 'UpdateSection', JSON.stringify(section), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    delete(id: number): Promise<boolean> {
        return this.http.delete(this.url + 'DeleteSection/' + id)
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    addStudentToSection(studentSection: StudentSectionModel): Promise<boolean> {
        return this.http.post(this.url + 'AddStudentToSection', JSON.stringify(studentSection), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    deleteStudentFromSection(studentId: number, sectionId: number): Promise<boolean> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('studentId', studentId.toString());
        params.set('sectionId', sectionId.toString());
        return this.http.delete(this.url + 'DeleteStudentFromSection', { search: params })
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