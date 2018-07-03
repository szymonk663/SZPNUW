import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {SemesterService} from "../../services/semester.service";
import {SubjectService} from "../../services/subject.service";
import {AccountService} from "../../services/account.service";
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {SubjectModel} from '../../viewmodels/SubjectModel';

@Component({
    selector: "sections-student",
    templateUrl: "./sections-student.component.html"
})

export class SectionsStudentComponent implements OnInit {
    error: string = '';
    message: string = '';
    semesterId: number;
    subjectId: number | null;
    semesters: SemesterModel[];
    subjects: SubjectModel[];

    constructor(private semesterService: SemesterService,
        private subjectService: SubjectService,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        const pId = this.accountService.getPId();
        if (pId !== null)
            this.semesterService
                .getSemestersByStudentId(pId)
                .then(semesters => this.semesters = semesters, error => this.error = error);
        const actualSectionsStu = localStorage.getItem('actualSectionsStu');
        if (actualSectionsStu !== null) {
            this.onSelectSemester(JSON.parse(actualSectionsStu).semesterId);
            this.subjectId = JSON.parse(actualSectionsStu).subjectId;
        }
    }

    onSelectSemester(semesterId: number) {
        this.semesterId = semesterId;
        this.subjectId = null;
        this.subjectService
            .getSubjectsBySemester(this.semesterId)
            .then(subjects => this.subjects = subjects, error => this.error = error);
    }

    onSelectSubject(subjectId: number) {
        this.subjectId = subjectId;
        localStorage.setItem('actualSectionsStu', JSON.stringify({ 'semesterId': this.semesterId, 'subjectId': this.subjectId }))
    }

    clear() {
        this.error = '';
        this.message = '';
    }
}