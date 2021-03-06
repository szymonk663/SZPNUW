﻿import { Injectable } from '@angular/core';
import { Http, Headers, Response, URLSearchParams } from '@angular/http';
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
import { Result } from '../viewmodels/Result';
import { UserModel } from '../viewmodels/UserModel';

@Injectable()
export class AccountService {
    constructor(private http: Http) { }

    private url = 'Account/';
    private headers = new Headers({ 'Content-Type': 'application/json' });
    private auth: Auth = new Auth(null, null, 0, false, '', '');
    private loggedIn: boolean;

    private handleError(error: Response): Promise<any> {
        return Promise.reject(error.text());
    }

    //Authentication and authorizatrion
    
    getUserId(): number | null {
        if (this.auth === null) {
            this.auth = this.getAuthProfile();
        }
        return this.auth !== null ? this.auth.Id : null;
    }

    getPId(): number | null {
        if (this.auth === null) {
            this.auth = this.getAuthProfile();
        }
        return this.auth !== null && this.auth.PId ? this.auth.PId : null;
    }

    registerStudent(model: StudentModel): Promise<Result> {
        return this.http.post(this.url + 'RegisterUser', JSON.stringify(model), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    registerAdmin(model: UserModel): Promise<Result> {
        return this.http.post(this.url + 'RegisterAdmin', JSON.stringify(model), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    registerInstructor(model: InstructorModel): Promise<Result> {
        return this.http.post(this.url + 'RegisterInstructor', JSON.stringify(model), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    login(user: LoginModel): Observable<Auth> {
        return this.http.post(this.url + 'Login', JSON.stringify(user), { headers: this.headers })
            .map(response => {
                if (response.status == 200) {
                    this.auth = response.json() as Auth;
                    localStorage.setItem('currentUser', JSON.stringify(this.auth));
                    this.loggedIn = true;
                    return this.auth;
                }
            }).catch(this.handleError);
    }

    logout(): void {
        localStorage.removeItem('currentUser');
        this.http.get(this.url + "LogOut").toPromise();
        this.loggedIn = false;
        this.auth = new Auth(null, null, 0, false, '', '');
    }

    isLoggedIn(): boolean {
        return this.loggedIn;
    }
    setLoggedIn(): void {
        this.loggedIn = true;
    }
    getAuthProfile(): Auth {
        if (this.auth === null) {
            this.http.get(this.url + "GetAuth").toPromise().then(result => {
                if (result.status == 200) {
                    this.auth = result.json() as Auth;
                }
            });
        }
        return this.auth;
    }

    getAuthProfilePromise(): Promise<Auth> {
        return this.http.get(this.url + "GetAuth").toPromise().then(result => {
            if (result.status == 200) {
                this.auth = result.json() as Auth;
                if (this.auth)
                    this.loggedIn = true;
                return this.auth;
            }
            return null;
        }).catch(this.handleError);
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
        return this.http.get(this.url + "GetStudentById/" + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as StudentModel;
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
                return result.json() as number;
            }
            return null;
        }).catch(this.handleError);
    }
    getStudentSection(studentId: number, sectionId: number): Promise<StudentSectionModel> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('studentId', studentId.toString());
        params.set('sectionId', sectionId.toString());
        return this.http.get(this.url + 'GetSectionStudent', { search: params }).toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as StudentSectionModel;
            }
            return null;
        }).catch(this.handleError);
    }

    updateStudentSection(studSec: StudentSectionModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateStudentSection', JSON.stringify(studSec), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    updateStudent(student: StudentModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateStudent', JSON.stringify(student), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    changePassword(passwords: ChangePasswordsModel): Promise<Result> {
        return this.http.put(this.url + 'ChangePassword', JSON.stringify(passwords), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    getStudentsBySemester(id: number): Promise<StudentModel[]> {
        return this.http.get(this.url + 'GetStudentsBySemesterId/' + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    rewriteTheStudentsForTheNewSemester(semestersId: SemestersIdModel): Promise<Result> {
        return this.http.post(this.url + 'RewriteStudentsSemester', JSON.stringify(semestersId), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    updateTheStudentsSemester(semestersId: SemestersIdModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateStudentsSemester', JSON.stringify(semestersId), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    deleteStudentsSemester(semesterId: number): Promise<Result> {
        return this.http.delete(this.url + 'DeleteStudentsSemester/' + semesterId)
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    rewriteTheStudentForTheNewSemester(semestersId: SemestersIdModel): Promise<Result> {
        return this.http.post(this.url + 'RewriteStudentSemester', JSON.stringify(semestersId), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    updateTheStudentSemester(semestersId: SemestersIdModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateStudentSemester', JSON.stringify(semestersId), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    deleteStudentSemester(studentId: number, semesterId: number): Promise<Result> {
        let params: URLSearchParams = new URLSearchParams();
        params.set('studentId', studentId.toString());
        params.set('semesterId', semesterId.toString());
        console.log(params);
        return this.http.delete(this.url + 'DeleteStudentSemester/', { search: params })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    } 

    getInstructor(id: number): Promise<InstructorModel> {
        return this.http.get(this.url + "GetInstructor/" + id).toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as InstructorModel;
            }
            return null;
        }).catch(this.handleError);
    }

    getCurrentInstructor(): Promise<InstructorModel> {
        return this.http.get(this.url + "GetCurrentInstructor").toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as InstructorModel;
            }
            return null;
        }).catch(this.handleError);
    }

    getCurrentStudent(): Promise<StudentModel> {
        return this.http.get(this.url + "GetCurrentStudent").toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as StudentModel;
            }
            return null;
        }).catch(this.handleError);
    }

    getInstructors(): Promise<InstructorModel[]> {
        return this.http.get(this.url + "GetInstructors").toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getAdmins(): Promise<UserModel[]> {
        return this.http.get(this.url + "GetAdmins").toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    updateInstructor(instructor: InstructorModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateInstructor', JSON.stringify(instructor), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    getCurrentAdmin(): Promise<UserModel> {
        return this.http.get(this.url + "GetCurrentAdmin").toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as UserModel;
            }
            return null;
        }).catch(this.handleError);
    }

    updateAdmin(user: UserModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateAdmin', JSON.stringify(user), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }
}