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
            this.auth = item !== null ? <Auth>JSON.parse(item) : new Auth(0, '');
            if (this.auth.permissions == "u")
                return true;
        }
        this.router.navigate(['/login']);
        return false;
    }
}