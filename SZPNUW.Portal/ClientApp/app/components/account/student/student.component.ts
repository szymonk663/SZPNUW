import { Component } from "@angular/core";
import { StudentModel } from "../../../viewmodels/StudentModel";
import { Router } from "@angular/router";
import { AccountService } from "../../../services/account.service";


@Component({
    selector: "student",
    templateUrl: "./student.component.html"
})

export class StudentComponent {
    public error = '';

    public student: StudentModel;

    constructor(private router: Router, private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.getCurrentStudent().then(student => { this.student = student; console.log(student)},
            reject => this.error = reject);
    }

    edit() {
        this.router.navigateByUrl("/student/detail");
    }
}