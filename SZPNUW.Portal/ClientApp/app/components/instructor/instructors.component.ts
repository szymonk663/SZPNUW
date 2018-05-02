import {Component, OnInit, Input} from "@angular/core";
import {Router} from "@angular/router";

import {Instructor} from '../../viewmodels/Instructor';
import {InstructorService} from "../../services/instructor.service";
import {SemesterService} from "../../services/semester.service";

@Component({
    selector: "isntructors",
    templateUrl: "template/instructor/instructors.component.html"
})

export class InstructorsComponent implements OnInit {
    private error = '';
    private instructors: Instructor[];
    private selectedInstructor: Instructor;

    constructor(private router: Router, private instructorService: InstructorService) { }

    ngOnInit() {
        this.instructorService.getInstructors().then(
            instructors => this.instructors = instructors,
            reject => this.error = reject
        );
    }
    onSelect(instructor: Instructor): void {
        this.selectedInstructor = instructor;
    }
}