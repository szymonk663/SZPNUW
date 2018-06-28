import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import { SemesterService } from "../../services/semester.service";
import { AccountService } from "../../services/account.service";
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {StudentModel} from '../../viewmodels/StudentModel';

@Component({
    selector: "student-list",
    templateUrl: "./student-list.component.html"
})

export class StudentListComponent implements OnInit {
    departments: string[];
    department: string = '';
    years: string[];
    year: string = '';
    semesters: SemesterModel[];
    students: StudentModel[];
    semesterId: number;
    selectedStudent: StudentModel;
    error: string = '';
    message: string = '';
    constructor(private accountService: AccountService,
        private semesterService: SemesterService,
        private router: Router
    ) { }

    ngOnInit() {
        this.semesterService.getSemesters().then(semesters => this.semesters = semesters, error => this.error = error);
        this.semesterService.getDepartments().then(departments => this.departments = departments, error => this.error = error);
        this.semesterService.getYears().then(years => this.years = years, error => this.error = error);
        if (localStorage.getItem('actualStudentList')) {
            const actualStudentList = localStorage.getItem('actualStudentList');
            this.semesterId = actualStudentList !== null ? JSON.parse(actualStudentList).semesterId : 0;
            this.onSend();
        }
    }

    onSelect(student: StudentModel): void {
        this.selectedStudent = student;
    }

    onDetail() {
        this.router.navigate(['/student/detail', this.selectedStudent.Id])
    }

    onSend() {
        this.clear();
        this.accountService
            .getStudentsBySemester(this.semesterId)
            .then(students => this.students = students,
            error => this.error = error);
        localStorage.setItem('actualStudentList', JSON.stringify({ 'semesterId': this.semesterId}))
    }

    delete() {
        this.clear();
        this.accountService
            .deleteStudentsSemester(this.semesterId)
            .then(result => {
                this.message = 'Studenci zostali wypisani z tego semestru.';
                this.onSend();
            },
                error => this.error = error);
    }

    private clear() {
        this.error = '';
        this.message = '';
    }
}