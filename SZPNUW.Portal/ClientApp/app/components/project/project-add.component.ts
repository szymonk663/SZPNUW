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
        let id_instructor = this.accountService.getUserId();
        this.project = new ProjectModel(0, '', '', id_instructor, this.subjectId, null);
    }

    onAdd() {
        this.onClear();
        this.projectService.addNew(this.project).then(result => {
            this.message = 'Temat projektu został dodany.';
            this.projectsListComponent.onRefresh();
        },
            error => this.error = error);
    }

    private onClear() {
        this.error = '';
        this.message = '';
    }
}