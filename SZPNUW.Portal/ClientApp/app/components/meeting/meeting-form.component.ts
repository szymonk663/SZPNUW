import {Component, Input, OnChanges} from "@angular/core";
import {Router} from "@angular/router"
import {MeetingService} from '../../services/meeting.service';
import {SectionService} from '../../services/section.service';
import {MeetingsComponent} from './meetings.component';
import {MeetingModel} from '../../viewmodels/MeetingModel';

@Component({
    selector: "meeting-form",
    templateUrl: "./meeting-form.component.html"
})

export class MeetingFormComponent implements OnChanges {
    @Input()
    meeting: MeetingModel;
    @Input()
    studentId: number;
    @Input()
    sectionId: number;
    edit: boolean;
    error: string = '';
    message: string = '';

    constructor(private meetingService: MeetingService,
        private meetingsComponent: MeetingsComponent,
        private sectionService: SectionService
    ) { }

    ngOnChanges() {
        this.onClear();
        if (this.meeting)
            this.edit = true;
        else {
            this.edit = false;
            this.sectionService
                .getSectionStudentId(this.sectionId, this.studentId)
                .then(result => this.meeting = new MeetingModel(0, result, null, null), error => this.error = error);  
        }
    }

    onAdd() {
        this.onClear();
        this.meetingService
            .AddMeeting(this.meeting)
            .then(result => {
                this.message = 'Spotkanie zostało dodane.';
                this.meetingsComponent.onRefresh();
            }, error => this.error = error);
    }

    onEdit() {
        this.onClear();
        this.meetingService
            .update(this.meeting)
            .then(result => {
                this.message = 'Spotkanie zostało zaktualizowane.';
                this.meetingsComponent.onRefresh();
            }, error => this.error = error);
    }

    onClear() {
        this.error = '';
        this.message = '';
    }
}