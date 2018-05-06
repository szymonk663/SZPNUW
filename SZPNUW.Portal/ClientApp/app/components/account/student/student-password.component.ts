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
        const userId = this.accountService.getUserId()
        this.passwords = new ChangePasswordsModel(userId !== null ? userId : 0, '', '');
    }

    onSubmit(): void {
        this.error = '';
        if (this.passwords.newPassword == this.retryPassword) {
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