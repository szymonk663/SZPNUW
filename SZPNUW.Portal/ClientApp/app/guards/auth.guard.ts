import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { Auth } from '../viewmodels/Auth'
import { AccountService } from '../services/account.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private accountService: AccountService) { }

    private auth: Auth | null = null;
    canActivate() {
        if (this.auth === null) {
            this.auth = this.accountService.getAuthProfile();
        }
        if (this.auth !== null) {
            if (this.auth.UserType == 2)
                return true;
        }
        this.router.navigate(['/login']);
        return false;
    }
}