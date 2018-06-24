import { Component, OnInit} from "@angular/core";
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AccountService } from "../../services/account.service";
import { ChangePasswordsModel } from "../../viewmodels/ChangePasswordModel";

@Component({
    templateUrl: '../account/student/student-password.component.html',
})

export class InstructorPasswordComponent implements OnInit {
    passwords: ChangePasswordsModel;
    retryPassword: string = '';
    error = '';

    constructor(private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.passwords = new ChangePasswordsModel('', '');
    }

    onSubmit(): void {
        this.error = '';
        if (this.passwords.NewPassword == this.retryPassword) {
            this.accountService.changePassword(this.passwords).then(result => {
                if (result != null) {
                    if (result.IsSucceeded)
                        this.goBack();
                    else
                        this.error = result.ErrorMessages;
                }
            }, error => this.error = error);
        }
        else
            this.error = 'Nowe hasło oraz powtórnie wpisane nowe hasło musi być takie same.';
    }

    goBack(): void {
        this.router.navigateByUrl("/instructor");
    }
}