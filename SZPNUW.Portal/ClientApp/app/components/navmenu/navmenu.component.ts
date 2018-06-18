import { Component } from '@angular/core';
import { AccountService } from "../../services/account.service";
import { Auth } from '../../viewmodels/Auth';
import { Router } from '@angular/router';
import { AppComponent } from '../app/app.component';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    constructor(private appComponent: AppComponent) {
        
    }

}
