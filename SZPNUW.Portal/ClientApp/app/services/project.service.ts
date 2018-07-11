import { Injectable } from '@angular/core';
import { Http, Headers, Response} from '@angular/http';
import 'rxjs/add/operator/map'

import {ProjectModel} from '../viewmodels/ProjectModel';
import {ProjectInstructorModel} from '../viewmodels/ProjectInstructorModel';
import {ProjectSubjectModel} from '../viewmodels/ProjectSubjectModel';
import { Result } from '../viewmodels/Result';

@Injectable()
export class ProjectService {
    constructor(private http: Http) { }

    private url = 'Project/';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    getProjectsBySubjectId(subjectId: number): Promise<ProjectInstructorModel[]> {
        return this.http.get(this.url + 'GetBySubjectId/' + subjectId).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getProjectsByInstructorId(instructorId: number): Promise<ProjectSubjectModel[]> {
        return this.http.get(this.url + 'GetProjectSubjectByInstructorId/' + instructorId).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getProjectsBySectionId(sectionId: number): Promise<ProjectInstructorModel[]> {
        return this.http.get(this.url + 'GetProjectsBySectionId/' + sectionId).toPromise().then(result => {
            if (result.status == 200) {
                return result.json();
            }
            return null;
        }).catch(this.handleError);
    }

    getProjectBySectionId(sectionId: number): Promise<ProjectModel> {
        return this.http.get(this.url + 'GetProjectBySectionId/' + sectionId).toPromise().then(result => {
            if (result.status == 200) {
                return result.json() as ProjectModel;
            }
            return null;
        }).catch(this.handleError);
    }

    addProject(project: ProjectModel): Promise<Result> {
        return this.http.post(this.url + 'AddProject', JSON.stringify(project), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    updateProject(project: ProjectModel): Promise<Result> {
        return this.http.put(this.url + 'UpdateProject', JSON.stringify(project), { headers: this.headers })
            .toPromise()
            .then(result => {
                if (result.status == 200)
                    return result.json() as Result;
                return null;
            }).catch(this.handleError);
    }

    deleteProject(projectId: number): Promise<Result> {
        return this.http.delete(this.url + 'DeleteProject/' + projectId)
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