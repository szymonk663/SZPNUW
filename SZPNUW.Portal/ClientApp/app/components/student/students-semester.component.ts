import {Component, Input, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {SemesterService} from "../../services/semester.service";
import {AccountService} from "../../services/account.service";
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {SemestersIdModel} from '../../viewmodels/SemestersIdModel';


@Component({
    selector: "students-semester",
    templateUrl: "./students-semester.component.html"
})

export class StudentsSemesterComponent implements OnInit {
    @Input()
    departments: string[];
    department: string = '';
    @Input()
    years: string[];
    year: string = '';
    @Input()
    semesters: SemesterModel[];
    @Input()
    selectedSemesterId: number;
    semestersId: SemestersIdModel;
    error: string = '';
    message: string = '';

    constructor(private accountService: AccountService,
        private semesterService: SemesterService,
        private router: Router
    ) {
        this.semestersId = new SemestersIdModel(0, null, null);
    }

    ngOnInit() {
        this.clear();
    }

    add() {
        if (this.check()) {
            this.accountService
                .rewriteTheStudentsForTheNewSemester(this.semestersId)
                .then(result => this.message = 'Studenci zostali dopisani do nowego semestru.',
                    error => this.error = error);
        }
        console.log(this.semestersId);
    }

    edit() {
        if (this.check()) {
            this.accountService
                .updateTheStudentsSemester(this.semestersId)
                .then(result => this.message = 'Wpis został zmodyfikowany, studenci zostali przepisani z poprzedniego semestru na nowy.',
                    error => this.error = error);
        }
    }

    private check(): boolean {
        this.clear();
        this.semestersId.id_semester = this.selectedSemesterId;
        if (this.semestersId.id_semester == this.semestersId.id_semesterNew) {
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