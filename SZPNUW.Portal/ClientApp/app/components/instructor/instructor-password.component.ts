import { Component, OnInit} from "@angular/core";
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { InstructorService } from "../../services/instructor.service";
import { Passwords } from "../../viewmodels/Passwords";

@Component({
    templateUrl: '/template/authentication/passwords.components.html',
})

export class InstructorPasswordComponent implements OnInit {
    passwords: Passwords;
    retryPassword: string = '';
    error = '';

    constructor(private route: ActivatedRoute,
        private location: Location,
        private instructorService: InstructorService
    ) { }

    ngOnInit() {
        this.passwords = new Passwords(this.instructorService.getInstructorId(), '', '');
    }

    onSubmit(): void {
        this.error = '';
        if (this.passwords.newPassword == this.retryPassword) {
            this.instructorService.newPassword(this.passwords).then(result => {
                if (result)
                    this.goBack();
            }, error => this.error = error);
        }
        else
            this.error = 'Nowe hasło oraz powtórnie wpisane nowe hasło musi być takie same.';
    }

    goBack(): void {
        this.location.back();
    }
}