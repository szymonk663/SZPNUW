import {Component, Input, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {ProjectService} from "../../services/project.service";
import {AccountService} from "../../services/account.service";
import {ProjectsListInstructorComponent} from './projects-list-instructor.component';

import {ProjectModel} from '../../viewmodels/ProjectModel';


@Component({
    selector: 'project-edit',
    templateUrl: "./project-form.component.html"
})

export class ProjectEditComponent implements OnInit {
    @Input()
    subjectId: number;
    @Input()
    project: ProjectModel;
    flag: boolean = false;
    error: string = '';
    message: string = '';

    constructor(private projectService: ProjectService,
        private accountService: AccountService,
        private projectsListInstructorComponent: ProjectsListInstructorComponent
    ) { }

    ngOnInit() {
        this.onClear();
        this.flag = true;
    }

    onEdit() {
        this.onClear();
        this.projectService.updateProject(this.project).then(result => {
            if (result != null) {
                if (result.IsSucceeded) {
                    this.message = 'Temat projektu został zmodyfikowany.';
                    this.projectsListInstructorComponent.onRefresh();
                }
                else {
                    this.error = result.ErrorMessages;
                }
            }
        },
            error => this.error = error);
    }

    private onClear() {
        this.error = '';
        this.message = '';
    }
}