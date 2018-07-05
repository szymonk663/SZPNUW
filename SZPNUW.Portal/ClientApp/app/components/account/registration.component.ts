import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { AccountService } from "../../services/account.service";
import { SemesterService } from "../../services/semester.service";
import { SemesterModel } from "../../viewmodels/SemesterModel";
import { StudentModel } from "../../viewmodels/StudentModel";

@Component({
    selector: 'registration',
    templateUrl: '/registration.component.html',
})

export class RegistrationComponent implements OnInit {

    semesters: SemesterModel[];
    error = '';

    user: StudentModel = new StudentModel(null, null, '', '', '', '', '', null, '', '', '', new SemesterModel(0, '', 0, ''));
    constructor(private router: Router, private accountService: AccountService, private semesterService: SemesterService) {
    }

    ngOnInit() {
        this.semesterService.getSemesters().then(semesters => this.semesters = semesters);
    }
    onSubmit() {
        this.accountService.registerStudent(this.user).then(result => {
            if (result !== null)
                if (result.IsSucceeded) {
                    this.goBack();
                }
                else
                    this.error = result.ErrorMessages
        },
            reject => this.error = reject
        );
    }
    goBack(): void {
        this.router.navigate(['/']);
    }
}