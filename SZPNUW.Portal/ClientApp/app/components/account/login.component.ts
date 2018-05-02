import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../../services/account.service';
import { LoginModel } from '../../viewmodels/LoginModel';
import {AppComponent} from '../app/app.component'

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
        private appComponent: AppComponent,
        private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.logout();
    }

    login() {
        this.loading = true;
        this.accountService.login(this.user)
            .subscribe(result => {
                if (result === true) {
                    this.appComponent.SetLoggedIn();
                    this.appComponent.SetAuthProfile();
                    this.loading = false;
                    this.router.navigate(['/']);
                } 
            },
            error => {
                this.loading = false;
                this.error = error;
            });
    }
}