import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';

import { AccountService } from '../../services/account.service';
import { RegistrationModel } from '../../viewmodels/RegistrationModel';

@Component({
    selector: 'instructor-registration',
    templateUrl: './instructor-registration.component.html',
})

export class InstructorRegistrationComponent{

    error = '';

    user: RegistrationModel = new RegistrationModel('', '', '', '', '', new Date(), '', '', '', 0);
    constructor(private route: ActivatedRoute,
        private location: Location,
        private accountService: AccountService
    ) { }

    onSubmit() {
        this.accountService.registerInstructor(this.user).then(result => {
            if (result == true)
                this.goBack();
        },
            reject => this.error = reject
        );
    }
    goBack(): void {
        this.location.back();
    }
}