import {Component, OnInit} from "@angular/core";
import {Router, ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import {SemesterService} from "../../services/semester.service";
import {SubjectService} from "../../services/subject.service";
import {AccountService} from "../../services/account.service";
import {SemesterModel} from "../../viewmodels/SemesterModel";
import {SubjectModel} from '../../viewmodels/SubjectModel';
import {InstructorModel} from '../../viewmodels/InstructorModel';
import {SubjectSemesterModel} from '../../viewmodels/SubjectSemesterModel';


@Component({
    selector: "subjcet",
    templateUrl: "./subject.component.html"
})

export class SubjectComponent implements OnInit {
    change: boolean = false;
    subject: SubjectModel;
    semesters: SemesterModel[];
    selectedSemester: SemesterModel;
    instructor: InstructorModel;
    subsem: SubjectSemesterModel;
    error: string = '';

    constructor(private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        private semesterService: SemesterService,
        private subjectService: SubjectService,
        private accountService: AccountService
    ) { }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            let id = +params['id'];
            if (id)
                this.subjectService.getSubject(id).then(subject => {
                    this.subject = subject;
                    this.refresh();
                    if (this.subject.id_manager)
                        this.accountService
                            .getInstructor(this.subject.id_manager)
                            .then(instructor => this.instructor = instructor,
                                error => this.error = error);
                },
                    reject => this.error = reject);
        });
    }

    onSelect(semester: SemesterModel) {
        this.selectedSemester = semester;
    }

    onDelete() {
        this.subjectService
            .getSubjectSemester(this.subject.id, this.selectedSemester.id)
            .then(subsem => {
                this.subsem = subsem;
                this.subjectService.deleteSemester(this.subsem.id).then(() => this.refresh(),
                    error => this.error = error);
            }, error => this.error = error);

    }

    refresh() {
        this.semesterService
            .getSemestersBySubjectId(this.subject.id)
            .then(semesters => this.semesters = semesters,
            error => this.error = error);
    }

    onEdit() {
        this.router.navigate(['/subject/semester', this.subject.id, this.selectedSemester.id]);
    }

    onAdd() {
        this.router.navigate(['/subject/semester', this.subject.id]);
    }

    onBack() {
        this.router.navigate(['/subjects'])
    }

    onChange(flag: boolean) {
        this.change = flag;
    }
}