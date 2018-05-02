import { Component } from '@angular/core';
import { AccountService } from "../../services/account.service";
import { Router } from "@angular/router";
import { Auth } from '../../viewmodels/Auth';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    private loggedIn: boolean;
    private auth: Auth | null;
    constructor(private accountService: AccountService, private router: Router) {
        this.loggedIn = this.accountService.isLoggedIn();
        this.auth = this.accountService.getAuthProfile();
    }

    SetLoggedIn(): void {
        this.loggedIn = true;
    }
    SetAuthProfile(): void {
        this.auth = this.accountService.getAuthProfile();
    }
    Logout(): void {
        this.loggedIn = false;
        this.auth = null;
        this.accountService.logout();
        this.router.navigate(['/']);
    }
}
