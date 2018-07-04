import {Component, Input, OnChanges} from "@angular/core";
import {Router} from "@angular/router"
import {MeetingService} from '../../services/meeting.service';
import {MeetingModel} from '../../viewmodels/MeetingModel';

@Component({
    selector: "meetings",
    templateUrl: "./meetings.component.html"
})

export class MeetingsComponent implements OnChanges {
    @Input()
    sectionId: number;
    @Input()
    studentId: number;
    @Input()
    permission: boolean;
    meetings: MeetingModel[];
    selectedMeeting: MeetingModel;
    error: string = '';
    message: string = '';

    constructor(private meetingService: MeetingService
    ) { }

    ngOnChanges() {
        this.onRefresh();
    }

    onSelect(meeting: MeetingModel) {
        this.selectedMeeting = meeting;
    }


    onDelete() {
        this.meetingService
            .delete(this.selectedMeeting.Id)
            .then(result => {
                if (result !== null) {
                    if (result.IsSucceeded) {
                        this.onRefresh();
                    }
                    else
                        this.error = result.ErrorMessages;
                }
            } , error => this.error = error);
    }

    onRefresh() {
        this.clear();
        this.meetingService
            .getMeetingsBySectionStudent(this.sectionId, this.studentId)
            .then(meetings => this.meetings = meetings, error => this.error = error);
    }

    clear() {
        this.error = '';
        this.message = '';
    }
}