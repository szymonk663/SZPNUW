import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { AccountService } from '../../services/account.service';
import { InstructorModel } from '../../viewmodels/InstructorModel';

@Component({
    selector: 'instructor-registration',
    templateUrl: './instructor-registration.component.html',
})

export class InstructorRegistrationComponent{
    error = '';
    user: InstructorModel = new InstructorModel( null, null, '', '', '', '', '', new Date(), '', '', '');
    constructor(private route: ActivatedRoute,
        private location: Location,
        private accountService: AccountService
    ) { }

    onSubmit() {
        this.accountService.registerInstructor(this.user).then(result => {
            if (result !== null)
                if (result.IsSucceeded) {
                    this.goBack();
                }
                else
                    this.error = result.ErrorMessages
        },
            reject => this.error = reject
        );
    }
    goBack(): void {
        this.location.back();
    }
}