import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../../services/account.service';
import { LoginModel } from '../../viewmodels/LoginModel';
import { AppComponent } from '../app/app.component';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})

export class LoginComponent implements OnInit {
    user = new LoginModel('', '');
    loading = false;
    error = '';

    constructor(
        private router: Router,
        private navMenuComponent: AppComponent,
        private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.logout();
    }

    login() {
        this.loading = true;
        this.accountService.login(this.user)
            .subscribe(result => {
                if (result.IsSucceeded) {
                    this.navMenuComponent.SetLoggedIn();
                    this.navMenuComponent.SetAuthProfile();
                    this.router.navigate(['/']);
                } else {
                    this.error = result.ErrorMessages;
                }
                this.loading = false;
            },
            error => {
                this.loading = false;
                this.error = error;
            });
    }
}