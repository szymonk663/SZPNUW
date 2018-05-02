import { Component, OnInit} from "@angular/core";
import {ProjectService} from '../../services/project.service';
import {AccountService} from '../../services/account.service';
import {ProjectModel} from '../../viewmodels/ProjectModel';
import {ProjectSubjectModel} from '../../viewmodels/ProjectSubjectModel';

@Component({
    templateUrl: './projects-list-instructor.component.html',
})

export class ProjectsListInstructorComponent implements OnInit {
    instructorId: number | null;
    projectsSubjects: ProjectSubjectModel[];
    selectedProject: ProjectSubjectModel;
    message: string = '';
    error = '';

    constructor(
        private projectService: ProjectService,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.instructorId = this.accountService.getUserId();
        this.onRefresh();
    }

    onRefresh() {
        if (this.instructorId !== null)
            this.projectService
                .getProjectsByInstructorId(this.instructorId)
                .then(projectsSubjects => {
                    this.projectsSubjects = projectsSubjects;
                },
                error => this.error = error);
    }

    onSelect(projectSubject: ProjectSubjectModel) {
        this.selectedProject = projectSubject;
    }

    onDelete() {
        this.projectService.delete(this.selectedProject.project.id).then(result => {
            this.message = 'Projekt został usunięty.';
            this.onRefresh();
        },
            error => this.error = error);
    }

}