import { Component, OnInit} from "@angular/core";
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AccountService } from "../../services/account.service";
import { InstructorModel } from "../../viewmodels/InstructorModel";
import {SubjectModel} from "../../viewmodels/SubjectModel";
import {SubjectService} from '../../services/subject.service';
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {SemesterService} from '../../services/semester.service';


@Component({
    selector: 'subject-form',
    templateUrl: './subject-form.component.html',
})

export class SubjectFormComponent implements OnInit {
    departments: string[];
    department: string = '';
    years: string[];
    year: string = '';
    lastName: string = '';
    subject: SubjectModel;
    semesters: SemesterModel[];
    instructors: InstructorModel[];
    error = '';

    constructor(private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private subjectService: SubjectService,
        private semesterService: SemesterService
    ) { }

    ngOnInit() {
        this.subject = new SubjectModel(0, '', '', null, null);
        this.accountService
            .getInstructors()
            .then(instructors => this.instructors = instructors,
                reject => this.error = reject);
        this.semesterService
            .getSemesters()
            .then(semesters => this.semesters = semesters,
                reject => this.error = reject);
        this.semesterService
            .getDepartments()
            .then(departments => this.departments = departments,
                error => this.error = error);
        this.semesterService
            .getYears()
            .then(years => this.years = years,
                error => this.error = error);
    }

    onSubmit(): void {
        this.subjectService.addSubject(this.subject)
            .then(result => {
                if (result !== null)
                    if (result.IsSucceeded)
                        this.goBack();
                    else
                        this.error = result.ErrorMessages;
            }, error => this.error = error);
    }

    goBack(): void {
        this.router.navigateByUrl("/subjects");
    }
}