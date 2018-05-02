import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpModule} from '@angular/http';
import {RouterModule}  from '@angular/router';
import {FormsModule} from '@angular/forms';
import 'rxjs/Rx';

//Components
import {SubjectsComponent} from '../components/subject/subjects.component';
import {SubjectComponent} from '../components/subject/subject.component';
import {SubjectDetailComponent} from '../components/subject/subject-detail.component';
import {SubjectFormComponent} from '../components/subject/subject-form.component';
import {SubjectSemesterComponent} from '../components/subject/subject-semester.component';
//Modules
import {AccountModule} from './account.module';
import {SemesterModule} from './semester.module';
import {ProjectModule} from './project.module';
//Pipes
//Routings
import {AppRouting} from '../app.routing';
//Services
import {SubjectService} from '../services/subject.service';
//Guards

@NgModule({
    // directives, components, and pipes
    declarations: [
        SubjectsComponent,
        SubjectComponent,
        SubjectDetailComponent,
        SubjectFormComponent,
        SubjectSemesterComponent
    ],
    // modules
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting,
        AccountModule,
        SemesterModule,
        ProjectModule

    ],
    // providers
    providers: [
        SubjectService
    ],
    exports: [
        SubjectsComponent
    ]
})
export class SubjectModule { }