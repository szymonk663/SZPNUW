import { Component } from "@angular/core";
import { Router } from "@angular/router";

import { AccountService } from "../../../services/account.service";
import { UserModel } from "../../../viewmodels/UserModel";

@Component({
    selector: 'admin-registration',
    templateUrl: '/admin-registration.component.html',
})

export class AdminRegistrationComponent {
    error = '';

    user: UserModel = new UserModel(null, '', '', '', '', '', null, '', '');
    constructor(private router: Router, private accountService: AccountService) {
    }

    onSubmit() {
        this.accountService.registerAdmin(this.user).then(result => {
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
        this.router.navigate(['/admins']);
    }
}