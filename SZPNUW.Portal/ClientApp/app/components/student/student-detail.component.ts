import {Component, OnInit} from "@angular/core";
import {Router, ActivatedRoute, Params } from '@angular/router';
import {SemesterService} from "../../services/semester.service";
import {AccountService} from '../../services/account.service';
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {StudentModel} from '../../viewmodels/StudentModel';


@Component({
    selector: "student-detail",
    templateUrl: "./student-detail.component.html"
})

export class StudentDetailComponent implements OnInit {
    semesters: SemesterModel[];
    selectedSemester: SemesterModel;
    student: StudentModel;
    error: string = '';
    message: string = '';

    constructor(private router: Router,
        private route: ActivatedRoute,
        private semesterService: SemesterService,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            let id = +params['id'];
            if (id)
                this.accountService.getStudent(id).then(student => {
                    this.student = student;
                    this.refresh();
                },
                    reject => this.error = reject);
        });
    }

    onSelect(semester: SemesterModel) {
        this.selectedSemester = semester;
    }

    delete() {
        if (this.student !== null && this.student.Id != null) {
            this.clear();
            this.accountService
                .deleteStudentSemester(this.student.Id, this.selectedSemester.Id)
                .then(result => {
                    if (result !== null) {
                        if (result.IsSucceeded) {
                            this.message = 'Student został wypisany z tego semestru.';
                            this.refresh();
                        }
                        else
                            this.error = result.ErrorMessages;
                    }
                },
                error => this.error = error);
        }
    }

    refresh() {
        if (this.student !== null && this.student.Id != null) {
            this.clear();
            this.semesterService
                .getSemestersByStudentId(this.student.Id)
                .then(semesters => {
                    this.semesters = semesters;
                },
                error => this.error = error);
        }
    }

    onBack() {
        this.router.navigate(['/students'])
    }

    private clear() {
        this.error = '';
        this.message = '';
    }
}