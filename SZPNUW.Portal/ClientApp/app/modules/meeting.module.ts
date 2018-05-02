import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpModule} from '@angular/http';
import {RouterModule}  from '@angular/router';
import {FormsModule} from '@angular/forms';
import 'rxjs/Rx';

//Components
import {MeetingsComponent} from '../components/meeting/meetings.component';
import {MeetingFormComponent} from '../components/meeting/meeting-form.component';

//Modules
//Pipes
//Routings
import {AppRouting} from '../app.routing';
//Services
import {MeetingService} from '../services/meeting.service';

@NgModule({
    // directives, components, and pipes
    declarations: [
        MeetingsComponent,
        MeetingFormComponent
    ],
    // modules
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting,
    ],
    // providers
    providers: [
        MeetingService
    ],
    exports: [
        MeetingsComponent
    ]
})
export class MeetingModule { }