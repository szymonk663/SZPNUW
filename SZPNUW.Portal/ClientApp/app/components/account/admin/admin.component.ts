import {Component, OnInit, Input} from "@angular/core";
import {Router} from "@angular/router";

import { UserModel } from '../../../viewmodels/UserModel';
import {AccountService} from "../../../services/account.service";

@Component({
    selector: "admin",
    templateUrl: "./admin.component.html"
})

export class AdminComponent implements OnInit {
    private error = '';

    private user: UserModel;

    constructor(private router: Router, private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.getCurrentAdmin().then(user => this.user = user,
            reject => this.error = reject);
    }
}