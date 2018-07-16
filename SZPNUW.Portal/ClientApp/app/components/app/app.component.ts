import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { Auth } from '../../viewmodels/Auth';
import { Router } from '@angular/router';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    public loggedIn: boolean;
    public auth: Auth;
    constructor(private accountService: AccountService, private router: Router) {
        this.loggedIn = this.accountService.isLoggedIn();
        this.auth = this.accountService.getAuthProfile();
    }

    ngOnInit() {
        this.accountService.getAuthProfilePromise().then(auth => {
            this.auth = auth;
            if (this.auth)
                this.loggedIn = true;
        });
    }

    SetLoggedIn(): void {
        this.loggedIn = true;
    }
    SetAuthProfile(): void {
        this.auth = this.accountService.getAuthProfile();
    }
    Logout(): void {
        this.loggedIn = false;
        this.auth = new Auth(null, null, 0, false, '', '');
        this.accountService.logout();
        this.router.navigate(['/']);
    }
}
