import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { Auth } from '../viewmodels/Auth'

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    private auth: Auth;
    private item: string | null;
    canActivate() {
        if (localStorage.getItem('currentUser')) {
            this.item = localStorage.getItem('currentUser');
            this.auth = this.item !== null ? <Auth>JSON.parse(this.item) : new Auth(null, '', false, '', '');
            if (this.auth.Permissions == "a")
                return true;
        }
        this.router.navigate(['/login']);
        return false;
    }
}