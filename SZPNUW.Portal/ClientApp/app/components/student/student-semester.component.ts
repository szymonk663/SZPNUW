import {Component, Input, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {SemesterService} from "../../services/semester.service";
import {AccountService} from "../../services/account.service";
import {StudentDetailComponent} from './student-detail.component';
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {SemestersIdModel} from '../../viewmodels/SemestersIdModel';


@Component({
    selector: 'student-semester',
    templateUrl: "./student-semester.component.html"
})

export class StudentSemesterComponent implements OnInit {
    @Input()
    studentId: number;
    @Input()
    selectedSemesterId: number;
    departments: string[];
    department: string = '';
    years: string[];
    year: string = '';
    semesters: SemesterModel[];

    semestersId: SemestersIdModel;
    error: string = '';
    message: string = '';

    constructor(private accountService: AccountService,
        private semesterService: SemesterService,
        private router: Router,
        private studentDetailComponent: StudentDetailComponent
    ) {
    }

    ngOnInit() {
        if (this.studentId)
            this.semestersId = new SemestersIdModel(this.studentId, 0, null);
        else
            this.error = 'Wystąpił błąd wewnętrzny proszę powiadomić administratora.';
        this.semesterService.getSemesters().then(semesters => this.semesters = semesters, error => this.error = error);
        this.semesterService.getDepartments().then(departments => this.departments = departments, error => this.error = error);
        this.semesterService.getYears().then(years => this.years = years, error => this.error = error);

    }

    add() {
        this.accountService
            .rewriteTheStudentForTheNewSemester(this.semestersId)
            .then(result => {
                this.message = 'Student został dopisany do nowego semestru.'
                this.studentDetailComponent.refresh();
            },
            error => this.error = error);
    }

    edit() {
        this.semestersId.SemesterId = this.selectedSemesterId;
        if (this.check()) {
            this.accountService
                .updateTheStudentSemester(this.semestersId)
                .then(result => {
                    this.message = 'Student został przepisany z poprzedniego semestru na nowy.';
                    this.studentDetailComponent.refresh();
                },
                error => this.error = error);
        }
    }

    private check(): boolean {
        this.clear();
        this.semestersId.SemesterId = this.selectedSemesterId;
        if (this.semestersId.SemesterId == this.semestersId.NewSemesterId) {
            this.error = 'Próbujesz przypisać studentów do semestru do którego są już przypisani.';
            return false;
        }
        else
            return true;
    }

    private clear() {
        this.error = '';
        this.message = '';
    }
}