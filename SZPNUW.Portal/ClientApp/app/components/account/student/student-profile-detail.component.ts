import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { AccountService } from "../../../services/account.service";
import { StudentModel } from "../../../viewmodels/StudentModel";
import { StudentProfileComponent } from "./student-profile.component";

@Component({
    selector: 'student-profile-detail',
    templateUrl: '/template/account/student-profile-detail.component.html',
})

export class StudentProfileDetailComponent implements OnInit {

    student: StudentModel;
    error = '';

    constructor(private route: ActivatedRoute,
        private location: Location,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        const userId = this.accountService.getUserId();
        if(userId !== null)
            this.accountService.getStudent(userId).then(student => this.student = student,
                reject => this.error = reject);
    }

    onSubmit(): void {
        this.accountService.updateStudent(this.student)
            .then(result => {
                this.goBack();
            }, error => this.error = error);
    }

    goBack(): void {
        this.location.back();
    }
}