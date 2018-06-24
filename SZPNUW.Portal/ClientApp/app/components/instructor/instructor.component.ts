import {Component, OnInit, Input} from "@angular/core";
import {Router} from "@angular/router";

import {InstructorModel} from '../../viewmodels/InstructorModel';
import {AccountService} from "../../services/account.service";
import {StudentProfileComponent} from '../account/student/student-profile.component';

@Component({
    selector: "instructor",
    templateUrl: "./instructor.component.html"
})

export class InstructorComponent implements OnInit {
    private error = '';

    private instructor: InstructorModel;

    constructor(private router: Router, private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.getCurrentInstructor().then(instructor => this.instructor = instructor,
            reject => this.error = reject);
    }
}