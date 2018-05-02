import { Component, OnInit} from "@angular/core";
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { InstructorService } from "../../services/instructor.service";
import { Instructor } from "../../viewmodels/Instructor";

@Component({
    selector: 'instructor-form',
    templateUrl: '/template/instructor/instructor-form.component.html',
})

export class InstructorFormComponent implements OnInit {

    instructor: Instructor;
    error = '';

    constructor(private route: ActivatedRoute,
        private location: Location,
        private instructorService: InstructorService
    ) { }

    ngOnInit() {
        this.instructorService.getInstructor(this.instructorService.getInstructorId())
            .then(instructor => this.instructor = instructor,
                reject => this.error = reject);
    }

    onSubmit(): void {
        this.instructorService.update(this.instructor)
            .then(result => {
                this.goBack();
            }, error => this.error = error);
    }

    goBack(): void {
        this.location.back();
    }
}