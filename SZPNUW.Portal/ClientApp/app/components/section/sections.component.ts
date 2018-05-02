import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {SemesterService} from "../../services/semester.service";
import {SubjectService} from "../../services/subject.service";
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {SubjectModel} from '../../viewmodels/SubjectModel';

@Component({
    selector: "sections",
    templateUrl: "./sections.component.html"
})

export class SectionsComponent implements OnInit {
    departments: string[];
    years: string[];
    department: string = '';
    year: string = '';
    error: string = '';
    message: string = '';
    semesterId: number | null;
    subjectId: number | null;
    semesters: SemesterModel[];
    subjects: SubjectModel[];

    constructor(private semesterService: SemesterService,
        private subjectService: SubjectService
    ) { }

    ngOnInit() {
        this.semesterService.getSemesters().then(semesters => this.semesters = semesters, error => this.error = error);
        this.semesterService.getDepartments().then(departments => this.departments = departments, error => this.error = error);
        this.semesterService.getYears().then(years => this.years = years, error => this.error = error);
        const actualSectionIns = localStorage.getItem('actualSectionsIns');
        if (actualSectionIns !== null) {
            this.onSelectSemester(JSON.parse(actualSectionIns).semesterId);
            this.subjectId = JSON.parse(actualSectionIns).subjectId;
        }
    }

    onSelectSemester(semesterId: number) {
        this.semesterId = semesterId;
        this.subjectId = null;
        this.subjectService.getSubjectsBySemester(this.semesterId).then(subjects => this.subjects = subjects, error => this.error = error);
    }

    onSelectSubject(subjectId: number) {
        this.subjectId = subjectId;
        localStorage.setItem('actualSectionsIns', JSON.stringify({ 'semesterId': this.semesterId, 'subjectId': this.subjectId }))
    }

    clear() {
        this.error = '';
        this.message = '';
    }
}