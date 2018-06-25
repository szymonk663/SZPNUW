import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {SemesterService} from "../../services/semester.service";
import {SubjectService} from "../../services/subject.service";
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {SubjectModel} from "../../viewmodels/SubjectModel";


@Component({
    selector: "subjects",
    templateUrl: "./subjects.component.html"
})

export class SubjectsComponent implements OnInit {
    departments: string[];
    department: string = '';
    years: string[];
    semestersNum: number[];
    semesterNum: number;
    subjects: SubjectModel[];
    selectedSubject: SubjectModel;
    error: string = '';
    constructor(private semesterService: SemesterService, private router: Router, private subjectService: SubjectService) { }

    ngOnInit() {
        this.semesterService.getDepartments().then(departments => this.departments = departments, error => this.error = error);
        this.semesterService.getSemestersNum().then(semestersNum => this.semestersNum = semestersNum, error => this.error = error);
        const actualSubject = localStorage.getItem('actualSubjects');
        if (actualSubject !== null) {
            this.department = JSON.parse(actualSubject).department;
            this.semesterNum = JSON.parse(actualSubject).semesterNum;
            this.onSend();
        }
    }

    onSend() {
        this.error = '';
        this.subjectService
            .getSubjects(this.department, this.semesterNum)
            .then(subjects => this.subjects = subjects, error => this.error = error);
        localStorage.setItem('actualSubjects', JSON.stringify({ 'department': this.department, 'semesterNum': this.semesterNum }))
    }

    add() {
        this.router.navigate(['/newsubject']);
    }

    onSelect(subject: SubjectModel): void {
        this.selectedSubject = subject;
    }
    onDetail() {
        this.router.navigate(['/subject', this.selectedSubject.Id]);
    }
}