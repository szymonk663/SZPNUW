import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { Auth } from '../viewmodels/Auth'

@Injectable()
export class StudentGuard implements CanActivate {

    constructor(private router: Router) { }

    private auth: Auth;

    canActivate() {
        if (localStorage.getItem('currentUser')) {
            const item = localStorage.getItem('currentUser');
            this.auth = item !== null ? <Auth>JSON.parse(item) : new Auth(null, null, false, '', '');
            if (this.auth.UserType == 1)
                return true;
        }
        this.router.navigate(['/login']);
        return false;
    }
}