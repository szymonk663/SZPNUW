import { Component, OnInit} from "@angular/core";
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { SemesterService } from "../../services/semester.service";
import { SemesterModel } from "../../viewmodels/SemesterModel";

@Component({
    templateUrl: './semester-detail.component.html',
})

export class SemesterDetailComponent implements OnInit {
    semesters: SemesterModel[];
    department: string = '';
    id: number;
    semester: SemesterModel;
    error = '';

    constructor(private route: ActivatedRoute,
        private location: Location,
        private semesterService: SemesterService
    ) { }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            this.id = +params['id'];
            if (this.id)
                this.semesterService.getSemester(this.id).then(semester => this.semester = semester,
                    reject => this.error = reject);
            else
                this.semester = new SemesterModel(0, '', null, '');
        });
        this.semesterService.getSemesters().then(semesters => this.semesters = semesters);
    }

    onSubmit(): void {
        this.semesterService.new(this.semester)
            .then(result => {
                this.goBack();
            }, error => this.error = error);
    }

    change(): void {
        this.semesterService.update(this.semester)
            .then(result => {
                this.goBack();
            }, error => this.error = error);
    }

    goBack(): void {
        this.location.back();
    }
}