import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";

import { UserModel } from '../../../viewmodels/UserModel';
import { AccountService } from "../../../services/account.service";

@Component({
    selector: "admin-list",
    templateUrl: "./admin-list.component.html"
})

export class AdminListComponent implements OnInit {
    private error = '';
    private users: UserModel[];
    private selectedUser: UserModel;

    constructor(private router: Router, private accountService: AccountService) { }

    ngOnInit() {
        this.accountService.getAdmins().then(
            users => this.users = users,
            reject => this.error = reject
        );
    }
    onSelect(user: UserModel): void {
        this.selectedUser = user;
    }
}