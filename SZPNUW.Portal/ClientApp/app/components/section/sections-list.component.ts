import {Component, Input, OnChanges} from "@angular/core";
import {Router} from "@angular/router"
import {SectionService} from '../../services/section.service';
import {AccountService} from "../../services/account.service";
import {ProjectService} from '../../services/project.service';
import {StudentSectionModel} from '../../viewmodels/StudentSectionModel';
import {StudentModel} from '../../viewmodels/StudentModel';
import {ProjectModel} from '../../viewmodels/ProjectModel';
import {SectionStudentsModel} from '../../viewmodels/SectionStudentsModel';

@Component({
    selector: "sections-list",
    templateUrl: "./sections-list.component.html"
})

export class SectionsListComponent implements OnChanges {
    @Input()
    semesterId: number;
    @Input()
    subjectId: number;
    selectedStudent: StudentModel | null;
    sections: SectionStudentsModel[] | null;
    selectedSection: SectionStudentsModel | null;
    error: string = '';
    message: string = '';
    project: ProjectModel;
    sectionStudent: StudentSectionModel;

    constructor(private sectionService: SectionService,
        private projectService: ProjectService,
        private accountService: AccountService,
        private router: Router
    ) { }

    ngOnChanges() {
        this.onRefresh();
    }

    onSelect(section: SectionStudentsModel) {
        this.selectedSection = section;
        this.getProject();
        this.selectedStudent = null;
    }

    onSelectStudent(student: StudentModel) {
        this.selectedStudent = student;
        this.getProject();
        this.getSectionStudent();
    }

    getProject() {
        if (this.selectedSection !== null)
        this.projectService
            .getProjectBySectionId(this.selectedSection.Section.Id)
            .then(project => this.project = project, error => this.error = error);
    }

    getSectionStudent() {
        if (this.selectedStudent !== null && this.selectedSection !== null && this.selectedStudent.Id !== null)
            this.accountService
                .getStudentSection(this.selectedStudent.Id, this.selectedSection.Section.Id)
                .then(result => this.sectionStudent = result, error => this.error = error);
    }

    goToReports() {
        if (this.selectedSection !== null)
            this.router.navigate(['/report/section', this.selectedSection.Section.Id])
    }

    onDelete() {
        if (this.selectedSection !== null)
            this.sectionService
                .delete(this.selectedSection.Section.Id)
                .then(result => {
                    if (result !== null)
                        if (result.IsSucceeded) {
                            this.onRefresh();
                        }
                        else
                            this.error = result.ErrorMessages;
                }, error => this.error = error);
    }

    onRefresh() {
        this.sections = null;
        this.selectedSection = null;
        this.selectedStudent = null;
        this.clear();
        this.sectionService
            .getSectionsStudents(this.subjectId, this.semesterId)
            .then(sections => this.sections = sections, error => this.error = error);
    }

    clear() {
        this.error = '';
        this.message = '';
    }
}