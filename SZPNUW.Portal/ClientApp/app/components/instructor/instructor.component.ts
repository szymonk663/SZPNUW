import {Component, OnInit, Input} from "@angular/core";
import {Router} from "@angular/router";

import {Instructor} from '../../viewmodels/Instructor';
import {InstructorService} from "../../services/instructor.service";
import {StudentProfileComponent} from '../account/student-profile.component';

@Component({
    selector: "instructor",
    templateUrl: "template/instructor/instructor.component.html"
})

export class InstructorComponent implements OnInit {
    private error = '';

    private instructor: Instructor;

    constructor(private router: Router, private instructorService: InstructorService) { }

    ngOnInit() {
        this.instructorService.getInstructor(this.instructorService.getInstructorId()).then(instructor => this.instructor = instructor,
            reject => this.error = reject);
    }
}