import {Component, Input, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {ProjectService} from "../../services/project.service";
import {AccountService} from "../../services/account.service";
import {ProjectsListComponent} from './projects-list.component';

import {ProjectModel} from '../../viewmodels/ProjectModel';


@Component({
    selector: 'project-add',
    templateUrl: "./project-form.component.html"
})

export class ProjectAddComponent implements OnInit {
    @Input()
    subjectId: number;
    project: ProjectModel;
    flag: boolean = false;
    error: string = '';
    message: string = '';

    constructor(private projectService: ProjectService,
        private accountService: AccountService,
        private projectsListComponent: ProjectsListComponent
    ) { }

    ngOnInit() {
        this.onClear();
        this.flag = false;
        let userId = this.accountService.getUserId();
        this.project = new ProjectModel(0, '', '', userId, this.subjectId, null);
    }

    onAdd() {
        this.onClear();
        this.projectService.addProject(this.project).then(result => {
            if (result != null) {
                console.log(result);
                if (result.IsSucceeded) {
                    this.message = 'Temat projektu został dodany.';
                    this.projectsListComponent.onRefresh();
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