import {Component, OnChanges, Input} from "@angular/core";
import {AccountService} from '../../services/account.service';
import {StudentSectionModel} from '../../viewmodels/StudentSectionModel';
import {SectionsListComponent} from './sections-list.component';


@Component({
    selector: "student-rating",
    templateUrl: "./student-rating.component.html"
})

export class StudentRatingsComponent implements OnChanges {
    @Input()
    sectionStudent: StudentSectionModel;
    average: number;
    error: string = '';
    message: string = '';
    studSec: StudentSectionModel;

    constructor(private accountService: AccountService,
        private sectionsListComponent: SectionsListComponent
    ) { }

    ngOnChanges() {
        this.studSec = new StudentSectionModel(
            this.sectionStudent.id,
            this.sectionStudent.sectionId,
            this.sectionStudent.studentId,
            this.sectionStudent.rate,
            this.sectionStudent.date);
        if (this.sectionStudent !== null && this.sectionStudent.studentId !== null)
            this.accountService
                .getStudentAverageRating(this.sectionStudent.studentId, this.sectionStudent.sectionId)
                .then(average => this.average = average, error => this.error = error);
    }

    onSubmit() {
        this.accountService.updateStudentSection(this.studSec).then(result => {
            this.message = 'Ocena została wpisana.';
            this.sectionsListComponent.getSectionStudent();
        }, error => this.error = error);
    }

    private onClear() {
        this.error = '';
        this.message = '';
    }
}