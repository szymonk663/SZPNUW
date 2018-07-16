import {Component, OnInit, Input} from "@angular/core";
import {Router} from "@angular/router";

import {InstructorModel} from '../../viewmodels/InstructorModel';
import {AccountService} from "../../services/account.service";
import {SemesterService} from "../../services/semester.service";

@Component({
    selector: "isntructors",
    templateUrl: "./instructors.component.html"
})

export class InstructorsComponent implements OnInit {
    public error = '';
    public instructors: InstructorModel[];
    public selectedInstructor: InstructorModel;

    constructor(private router: Router, private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.getInstructors().then(
            instructors => this.instructors = instructors,
                reject => this.error = reject
        );
    }
    onSelect(instructor: InstructorModel): void {
        this.selectedInstructor = instructor;
    }
}