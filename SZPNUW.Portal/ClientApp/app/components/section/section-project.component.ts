import {Component, Input, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {ProjectService} from "../../services/project.service";
import {SectionService} from "../../services/section.service";
import {SectionsListStudentComponent} from './sections-list-student.component';
import {ProjectInstructorModel} from "../../viewmodels/ProjectInstructorModel";
import {SectionModel} from '../../viewmodels/SectionModel';

@Component({
    selector: "section-project",
    templateUrl: "./section-project.component.html"
})

export class SectionProjectComponent implements OnInit {
    @Input()
    section: SectionModel;
    message: string = '';
    error: string = '';
    projects: ProjectInstructorModel[];
    selectedProject: ProjectInstructorModel;
    nullProject: ProjectInstructorModel | null = null;
    flag: boolean;

    constructor(private sectionService: SectionService,
        private projectService: ProjectService,
        private sectionListStudentComponent: SectionsListStudentComponent
    ) { }

    ngOnInit() {
        this.projectService.getProjectsBySectionId(this.section.id).then(projects => this.projects = projects, error => this.error = error);
    }

    onSelect(section: ProjectInstructorModel) {
        this.selectedProject = section;
        
    }

    onSubmit() {
        this.section.idProject = this.selectedProject.Project.Id;
        this.sectionService
            .update(this.section)
            .then(result => {
                this.message = 'Sekcji przypisano nowy temat.';
                this.sectionListStudentComponent.getProject();
            }, error => this.error = error);
    }

    onClear() {
        this.error = '';
        this.message = '';
    }
}