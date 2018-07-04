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
            this.sectionStudent.Id,
            this.sectionStudent.SectionId,
            this.sectionStudent.StudentId,
            this.sectionStudent.Rate,
            this.sectionStudent.Date);
        if (this.sectionStudent !== null && this.sectionStudent.StudentId !== null)
            this.accountService
                .getStudentAverageRating(this.sectionStudent.StudentId, this.sectionStudent.SectionId)
                .then(average => this.average = average, error => this.error = error);
    }

    onSubmit() {
        this.accountService.updateStudentSection(this.studSec).then(result => {
            if (result != null) {
                if (result.IsSucceeded) {
                    this.message = 'Ocena została wpisana.';
                    this.sectionsListComponent.getSectionStudent();
                }
                else
                    this.error = result.ErrorMessages;
            }
            
        }, error => this.error = error);
    }

    private onClear() {
        this.error = '';
        this.message = '';
    }
}