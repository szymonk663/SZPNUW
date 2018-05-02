﻿import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import { SemesterModel } from '../viewmodels/SemesterModel'
import { StudentModel } from '../viewmodels/StudentModel'
import { RegistrationModel } from '../viewmodels/RegistrationModel'
import { StudentSectionModel } from '../viewmodels/StudentSectionModel'
import { ChangePasswordsModel } from '../viewmodels/ChangePasswordModel'
import { SemestersIdModel } from '../viewmodels/SemestersIdModel'
import { InstructorModel } from '../viewmodels/InstructorModel'
import { Auth } from '../viewmodels/Auth'
import { LoginModel } from '../viewmodels/LoginModel'

import { AccountModule } from '../modules/account.module';

@Injectable()
export class AccountService {
    constructor(private http: Http) { }

    private url = 'Account/';
    private headers = new Headers({ 'Content-Type': 'application/json' });
    private auth: Auth | null;
    private loggedIn: boolean;

    private handleError(error: Response): Promise<any> {
        return Promise.reject(error.json());
    }

    //Authentication and authorizatrion
    
    getUserId(): number | null {
        const userJson = localStorage.getItem('currentUser');
        return userJson !== null ? JSON.parse(userJson).id : null;
    }

    registerStudent(model: RegistrationModel): Promise<Boolean> {
        return this.sendPost(this.url + 'RegisterStudent', model);
    }

    registerInstructor(model: RegistrationModel): Promise<Boolean> {
        return this.sendPost(this.url + "RegisterInstructor", model);
    }

    login(user: LoginModel): Observable<boolean> {
        return this.http.post(this.url + 'login', JSON.stringify(user), { headers: this.headers })
            .map(response => {
                if (response.status == 200) {
                    this.auth = <Auth>response.json();
                    localStorage.setItem('currentUser', JSON.stringify(this.auth));
                    this.loggedIn = true;
                    return true;
                }
            }).catch(this.handleError);
    }

    logout(): void {
        localStorage.removeItem('currentUser');
        this.loggedIn = false;
        this.auth = null;
    }

    isLoggedIn(): boolean {
        return this.loggedIn;
    }
    setLoggedIn(): void {
        this.loggedIn = true;
    }
    getAuthProfile(): Auth | null {
        return this.auth;
    }

    private sendPost(url: string, user: RegistrationModel): Promise<Boolean> {
        return this.http.post(url, JSON.stringify(user), { headers: this.headers }).toPromise().then(response => {
            if (response.ok) {
                return true;
            }
            return false;
        }).catch(this.handleError);
    }

    //end of authentication and authorization

    getStudent(id: number): Promise<StudentModel> {
        return this.http.get(this.url + id).toPromise().then(result => {
            if (result.status == 200) {
                return <StudentModel>result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getStudentAverageRating(studentId: number, sectionId: number): Promise<number> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('studentId', studentId.toString());
        params.set('sectionId', sectionId.toString());
        return this.http.get(this.url + 'GetStudentAverageRating', { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return <number>result.json();
            }
            return null;
        }).catch(this.handleError);
    }
    getStudentSection(studentId: number, sectionId: number): Promise<StudentSectionModel> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('studentId', studentId.toString());
        params.set('sectionId', sectionId.toString());
        return this.http.get(this.url + 'GetStudentSection', { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return <StudentSectionModel>result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    updateStudentSection(studSec: StudentSectionModel): Promise<boolean> {
        return this.http.put(this.url + 'UpdateStudentSection', JSON.stringify(studSec), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    updateStudent(student: AccountModule): Promise<boolean> {
        return this.http.put(this.url + 'UpdateStudent', JSON.stringify(student), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    changePassword(passwords: ChangePasswordsModel): Promise<boolean> {
        return this.http.put(this.url + 'ChangePassword', JSON.stringify(passwords), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    getStudentsBySemester(id: number): Promise<StudentModel[]> {
        return this.http.get(this.url + 'GetStudentsBySemester/' + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    rewriteTheStudentsForTheNewSemester(semestersId: SemestersIdModel): Promise<boolean> {
        return this.http.post(this.url + 'RewriteStudentsSemester', JSON.stringify(semestersId), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    updateTheStudentsSemester(semestersId: SemestersIdModel): Promise<boolean> {
        return this.http.put(this.url + 'UpdateStudentsSemester', JSON.stringify(semestersId), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    deleteStudentsSemester(semesterId: number): Promise<boolean> {
        return this.http.delete(this.url + 'DeleteStudentsSemester/' + semesterId)
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    rewriteTheStudentForTheNewSemester(semestersId: SemestersIdModel): Promise<boolean> {
        return this.http.post(this.url + 'RewriteStudentSemester', JSON.stringify(semestersId), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    updateTheStudentSemester(semestersId: SemestersIdModel): Promise<boolean> {
        return this.http.put(this.url + 'UpdateStudentSemester', JSON.stringify(semestersId), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }

    deleteStudentSemester(studentId: number, semesterId: number): Promise<boolean> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('studentId', studentId.toString());
        params.set('semesterId', semesterId.toString());
        return this.http.delete(this.url + 'DeleteStudentSemester', { search: params })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return true;
                return false;
            }).catch(this.handleError);
    } 

    getInstructor(id: number): Promise<InstructorModel> {
        return this.http.get(this.url + id).toPromise().then(result => {
            if (result.status == 200) {
                return <InstructorModel>result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getInstructors(): Promise<InstructorModel[]> {
        return this.http.get(this.url).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    updateInstructor(instructor: InstructorModel): Promise<boolean> {
        return this.http.put(this.url + 'update', JSON.stringify(instructor), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 201)
                    return true;
                return false;
            }).catch(this.handleError);
    }
}