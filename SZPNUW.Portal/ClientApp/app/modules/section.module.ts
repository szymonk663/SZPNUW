import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpModule} from '@angular/http';
import {RouterModule}  from '@angular/router';
import {FormsModule} from '@angular/forms';
import 'rxjs/Rx';

//Components
import {SectionsListComponent} from '../components/section/sections-list.component';
import {SectionsComponent} from '../components/section/sections.component';
import {SectionsFormComponent} from '../components/section/sections-form.component';
import {SectionsListStudentComponent} from '../components/section/sections-list-student.component';
import {SectionsStudentComponent} from '../components/section/sections-student.component';
import {SectionProjectComponent} from '../components/section/section-project.component';
import {StudentRatingsComponent} from '../components/section/student-ratings.component';
//Modules
import {SemesterModule} from './semester.module';
import {MeetingModule} from './meeting.module';
import {ReportModule} from './report.module';
//Pipes
//Routings
import {AppRouting} from '../app.routing';
//Services
import {SectionService} from '../services/section.service';


@NgModule({
    // directives, components, and pipes
    declarations: [
        SectionsListComponent,
        SectionsComponent,
        SectionsFormComponent,
        SectionsStudentComponent,
        SectionsListStudentComponent,
        SectionProjectComponent,
        StudentRatingsComponent
    ],
    // modules
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting,
        SemesterModule,
        MeetingModule,
        ReportModule
    ],
    // providers
    providers: [
        SectionService
    ],
    exports: [
        SectionsComponent,
        SectionsStudentComponent
    ]
})
export class SectionModule { }