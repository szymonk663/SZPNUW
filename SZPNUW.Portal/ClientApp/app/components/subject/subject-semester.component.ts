import {Component, OnInit} from "@angular/core";
import {Router, ActivatedRoute, Params } from '@angular/router';
import {SemesterService} from "../../services/semester.service";
import {SubjectService} from "../../services/subject.service";
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {SubjectSemesterModel} from '../../viewmodels/SubjectSemesterModel';

@Component({
    selector: "subjcet-semester",
    templateUrl: "./subject-semester.component.html"
})

export class SubjectSemesterComponent implements OnInit {
    departments: string[];
    department: string = '';
    years: string[];
    year: string = '';
    semesters: SemesterModel[];
    subsem: SubjectSemesterModel;
    error: string = '';

    constructor(private router: Router,
        private route: ActivatedRoute,
        private semesterService: SemesterService,
        private subjectService: SubjectService
    ) { }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            let id_subject = +params['id_subject'];
            let id_semester = +params['id_semester']
            if (id_subject) {
                this.subsem = new SubjectSemesterModel(0, id_subject, null);
                if (id_semester) {
                    this.subjectService
                        .getSubjectSemester(id_subject, id_semester)
                        .then(subsem => { this.subsem = subsem; console.log(subsem)},
                            error => this.error = error);
                }
            }
        });
        this.semesterService
            .getSemesters()
            .then(semesters => this.semesters = semesters,
            error => this.error = error);
        this.semesterService.getDepartments().then(departments => this.departments = departments, error => this.error = error);
        this.semesterService.getYears().then(years => this.years = years, error => this.error = error);
    }

    onSubmit() {
        if (this.subsem.Id == 0)
            this.onAdd();
        else
            this.onEdit();
    }

    onAdd() {
        this.subjectService.addSemester(this.subsem)
            .then(result => {
                if (result !== null)
                    if (result.IsSucceeded)
                        this.goBack();
                    else
                        this.error = result.ErrorMessages;
            },
            error => this.error = error);
    }

    onEdit() {
        this.subjectService.updateSemester(this.subsem)
            .then(result => {
                if (result !== null)
                    if (result.IsSucceeded)
                        this.goBack();
                    else
                        this.error = result.ErrorMessages;
            },
            error => this.error = error);
    }

    goBack() {
        this.router.navigate(['/subject', this.subsem.SubjectId])
    }
}