import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Location } from '@angular/common';
import { AccountService } from "../../../services/account.service";
import { StudentModel } from "../../../viewmodels/StudentModel";
import { StudentProfileComponent } from "./student-profile.component";

@Component({
    selector: 'student-profile-detail',
    templateUrl: './student-profile-detail.component.html',
})

export class StudentProfileDetailComponent implements OnInit {

    student: StudentModel;
    error = '';

    constructor(private route: ActivatedRoute,
        private location: Location,
        private accountService: AccountService,
        private router: Router
    ) { }

    ngOnInit() {
        this.accountService.getCurrentStudent().then(student => this.student = student,
            reject => this.error = reject);
    }

    onSubmit(): void {
        this.accountService.updateStudent(this.student)
            .then(result => {
                if (result !== null)
                    if (result.IsSucceeded)
                        this.goBack();
                    else
                        this.error = result.ErrorMessages;
            }, error => this.error = error);
    }

    goBack(): void {
        this.router.navigateByUrl("/student");
    }
}