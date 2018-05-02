import { Component, OnInit, Input} from "@angular/core";
import {ProjectService} from '../../services/project.service';
import {ProjectModel} from '../../viewmodels/ProjectModel';
import {ProjectInstructorModel} from '../../viewmodels/ProjectInstructorModel';

@Component({
    selector: 'projects-list',
    templateUrl: './projects-list.component.html',
})

export class ProjectsListComponent implements OnInit {
    @Input()
    subjectId: number;
    projectsInstructors: ProjectInstructorModel[];
    selectedProject: ProjectInstructorModel;
    message: string = '';
    error = '';

    constructor(
        private projectService: ProjectService
    ) { }

    ngOnInit() {
        this.onRefresh();
    }

    onRefresh() {
        this.projectService
            .getProjectsBySubjectId(this.subjectId)
            .then(projectsInstructors => {
                this.projectsInstructors = projectsInstructors;
            },
            error => this.error = error);
    }

    onSelect(projectInstructor: ProjectInstructorModel) {
        this.selectedProject = projectInstructor;
    }
}