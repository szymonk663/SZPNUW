import {Component, Input, OnChanges, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {SectionService} from '../../services/section.service';
import {AccountService} from "../../services/account.service";
import {ProjectService} from "../../services/project.service";
import {StudentModel} from '../../viewmodels/StudentModel';
import {SectionStudentsModel} from '../../viewmodels/SectionStudentsModel';
import {StudentSectionModel} from '../../viewmodels/StudentSectionModel';
import {ProjectModel} from '../../viewmodels/ProjectModel';

@Component({
    selector: "sections-list-student",
    templateUrl: "./sections-list-student.component.html"
})

export class SectionsListStudentComponent implements OnChanges, OnInit {
    @Input()
    semesterId: number;
    @Input()
    subjectId: number;
    sections: SectionStudentsModel[] | null;
    selectedSection: SectionStudentsModel | null;
    error: string = '';
    message: string = '';
    studentId: number | null;
    storred: boolean = true;
    studentSection: StudentSectionModel = new StudentSectionModel(0, 0, 0, null, null);
    selectedStudent: StudentModel | null;
    project: ProjectModel;
    sectionStudent: StudentSectionModel | null;

    constructor(private sectionService: SectionService,
        private accountService: AccountService,
        private projectService: ProjectService,
        private router: Router
    ) { }

    ngOnInit() {
        this.studentId = this.accountService.getPId();
    }

    ngOnChanges() {
        this.onRefresh();
    }

    onSelect(section: SectionStudentsModel) {
        this.selectedSection = section;
        this.onFind();
        this.getProject();
        this.sectionStudent = null;
    }

    onSelectStudent(student: StudentModel) {
        this.selectedStudent = student;
        this.getSectionStudent();
    }

    getProject() {
        if (this.selectedSection !== null)
            this.projectService
                .getProjectBySectionId(this.selectedSection.Section.Id)
                .then(project => this.project = project, error => this.error = error);
    }

    getSectionStudent() {
        if (this.selectedSection !== null && this.selectedStudent !== null && this.selectedStudent.Id !== null)
            this.accountService
                .getStudentSection(this.selectedStudent.Id, this.selectedSection.Section.Id)
                .then(result => this.sectionStudent = result, error => this.error = error);
    }

    goToReports() {
        if (this.selectedSection !== null)
            this.router.navigate(['/report/section', this.selectedSection.Section.Id])
    }

    onDeleteProject() {
        if (this.selectedSection !== null) {
            this.selectedSection.Section.ProjectId = 0;
            this.sectionService.update(this.selectedSection.Section).then(response => this.getProject(), error => this.error = error);
        }
    }

    onFind() {
        if (this.selectedSection !== null && this.selectedSection.Students.length == 0) {
            this.storred = false;
            this.selectedSection.Students.forEach(student => {
                if (student.Id == this.studentId) {
                    this.storred = true;
                    return true;
                }
                else {
                    this.storred = false;
                }
            });
        }    
        return false;
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

    onAdd() {
        this.clear();
        if (this.selectedSection !== null) {
            this.studentSection.sectionId = this.selectedSection.Section.Id;
            this.studentSection.studentId = this.studentId;
        }
        this.sectionService
            .addStudentToSection(this.studentSection)
            .then(result => this.onRefresh(), error => this.error = error);
    }

    onDelete() {
        if (this.studentId !== null && this.selectedSection !== null) {
            this.clear();
            this.sectionService
                .deleteStudentFromSection(this.studentId, this.selectedSection.Section.Id)
                .then(result => this.onRefresh(), error => this.error = error);
        }
    }

    clear() {
        this.error = '';
        this.message = '';
    }
}