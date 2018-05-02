import { Component, OnInit} from "@angular/core";
import {Router, ActivatedRoute, Params } from '@angular/router';
import {SubjectService} from '../../services/subject.service';
import {AccountService} from '../../services/account.service';
import { SubjectModel } from '../../viewmodels/SubjectModel';
import {InstructorModel} from '../../viewmodels/InstructorModel';

@Component({
    templateUrl: './subject-detail.component.html',
})

export class SubjectDetailComponent implements OnInit {
    subject: SubjectModel;
    instructors: InstructorModel[];
    lastName: string = '';
    error = '';

    constructor(private router: Router,
        private route: ActivatedRoute,
        private subjectService: SubjectService,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            let id = +params['id'];
            if (id)
                this.subjectService
                    .getSubject(id)
                    .then(subject => this.subject = subject,
                    reject => this.error = reject);
        });
        this.accountService
            .getInstructors()
            .then(instructors => this.instructors = instructors,
                error => this.error = error);
    }

    onSubmit(): void {
        this.subjectService.update(this.subject)
            .then(result => {
                this.goBack();
            }, error => this.error = error);
    }

    goBack(): void {
        this.router.navigate(['/subject', this.subject.id]);
    }
}