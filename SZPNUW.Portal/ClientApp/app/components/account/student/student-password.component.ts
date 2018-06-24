import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { AccountService } from "../../../services/account.service";
import { ChangePasswordsModel } from "../../../viewmodels/ChangePasswordModel";

@Component({
    templateUrl: './student-password.component.html',
})

export class StudentPasswordComponent implements OnInit {
    passwords: ChangePasswordsModel;
    retryPassword: string = '';
    error = '';

    constructor(private route: ActivatedRoute,
        private location: Location,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.passwords = new ChangePasswordsModel('', '');
    }

    onSubmit(): void {
        this.error = '';
        if (this.passwords.NewPassword == this.retryPassword) {
            this.accountService.changePassword(this.passwords).then(result => {
                if (result)
                    this.goBack();
            }, error => this.error = error);
        }
        else
            this.error = 'Nowe hasło oraz powtórnie wpisane nowe hasło musi być takie same';
    }

    goBack(): void {
        this.location.back();
    }
}