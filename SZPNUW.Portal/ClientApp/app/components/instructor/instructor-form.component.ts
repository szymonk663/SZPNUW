import { Component, OnInit} from "@angular/core";
import { ActivatedRoute, Params, Router } from '@angular/router';
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
        private router : Router,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.accountService.getCurrentInstructor()
        .then(instructor => this.instructor = instructor,
            reject => this.error = reject);
    }

    onSubmit(): void {
        this.accountService.updateInstructor(this.instructor)
            .then(result => {
                if (result !== null)
                    if (result.IsSucceeded)
                        this.goBack();
                    else
                        this.error = result.ErrorMessages;
                this.goBack();
            }, error => this.error = error);
    }

    goBack(): void {
        this.router.navigateByUrl("/instructor");
    }
}