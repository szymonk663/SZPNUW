import { Component, OnInit} from "@angular/core";
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { AccountService } from "../../services/account.service";
import { InstructorModel } from "../../viewmodels/InstructorModel";

@Component({
    selector: 'instructor-form',
    templateUrl: './instructor-form.component.html',
})

export class InstructorFormComponent implements OnInit {

    instructor: InstructorModel;
    error = '';

    constructor(private route: ActivatedRoute,
        private location: Location,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        const userId = this.accountService.getUserId();
        if(userId != null)
            this.accountService.getInstructor(userId)
            .then(instructor => this.instructor = instructor,
                reject => this.error = reject);
    }

    onSubmit(): void {
        this.accountService.updateInstructor(this.instructor)
            .then(result => {
                this.goBack();
            }, error => this.error = error);
    }

    goBack(): void {
        this.location.back();
    }
}