import { Component, OnInit} from "@angular/core";
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AccountService } from "../../../services/account.service";
import { UserModel } from "../../../viewmodels/UserModel";

@Component({
    selector: 'admin-form',
    templateUrl: './admin-form.component.html',
})

export class AdminFormComponent implements OnInit {

    user: UserModel;
    error = '';

    constructor(private route: ActivatedRoute,
        private router : Router,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.accountService.getCurrentAdmin()
        .then(user => this.user = user,
            reject => this.error = reject);
    }

    onSubmit(): void {
        this.accountService.updateAdmin(this.user)
            .then(result => {
                if (result !== null)
                    if (result.IsSucceeded)
                        this.goBack();
                    else
                        this.error = result.ErrorMessages;
            }, error => this.error = error);
    }

    goBack(): void {
        this.router.navigateByUrl("/admin");
    }
}