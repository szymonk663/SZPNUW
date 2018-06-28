import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";

import { StudentModel } from '../../../viewmodels/StudentModel';
import { SemesterModel } from '../../../viewmodels/SemesterModel';
import { AccountService } from "../../../services/account.service";
import { SemesterService } from "../../../services/semester.service";

@Component({
    selector: "student-profile",
    templateUrl: "./student-profile.component.html"
})

export class StudentProfileComponent implements OnInit {
    private error = '';

    private student: StudentModel;
    private semester: SemesterModel;

    constructor(private router: Router, private accountService: AccountService, private semesterService: SemesterService) { }

    ngOnInit() {
        const userId = this.accountService.getUserId();
        if (userId !== null)
        this.accountService.getStudent(userId).then(student => {
            this.student = student;
            if (this.student.SemesterId !== null)
                this.semesterService.getSemestersById(this.student.SemesterId).then(semester => this.semester = semester,
                    reject => this.error = reject)
        },
            reject => this.error = reject
        );
    }

}