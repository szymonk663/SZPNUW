import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { AccountService } from "../../services/account.service";
import { SemesterService } from "../../services/semester.service";
import { RegistrationModel } from "../../viewmodels/RegistrationModel";
import { SemesterModel } from "../../viewmodels/SemesterModel";

@Component({
    selector: 'registration',
    templateUrl: '/registration.component.html',
})

export class RegistrationComponent implements OnInit {

    semesters: SemesterModel[];
    error = '';

    user: RegistrationModel = new RegistrationModel('', '', '', '', '', null, '', '', '', null);
    constructor(private router: Router, private accountService: AccountService, private semesterService: SemesterService) {
    }

    ngOnInit() {
        this.semesterService.getSemesters().then(semesters => this.semesters = semesters);
    }
    onSubmit() {
        this.accountService.registerStudent(this.user).then(result => {
            if (result == true)
                this.router.navigate(['/']);
        },
            reject => this.error = reject
        );
    }
}