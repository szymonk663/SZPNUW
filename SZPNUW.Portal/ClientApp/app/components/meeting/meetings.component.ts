import {Component, Input, OnChanges} from "@angular/core";
import {Router} from "@angular/router"
import {MeetingService} from '../../services/meeting.service';
import {Meeting} from '../../viewmodels/Meeting';

@Component({
    selector: "meetings",
    templateUrl: "template/meeting/meetings.component.html"
})

export class MeetingsComponent implements OnChanges {
    @Input()
    sectionId: number;
    @Input()
    studentId: number;
    @Input()
    permission: boolean;
    meetings: Meeting[];
    selectedMeeting: Meeting;
    error: string = '';
    message: string = '';

    constructor(private meetingService: MeetingService
    ) { }

    ngOnChanges() {
        this.onRefresh();
    }

    onSelect(meeting: Meeting) {
        this.selectedMeeting = meeting;
    }


    onDelete() {
        this.meetingService
            .delete(this.selectedMeeting.id)
            .then(() => this.onRefresh(), error => this.error = error);
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