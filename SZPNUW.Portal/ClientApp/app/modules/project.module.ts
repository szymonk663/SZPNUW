import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpModule} from '@angular/http';
import {RouterModule}  from '@angular/router';
import {FormsModule} from '@angular/forms';
import 'rxjs/Rx';

//Components
import {ProjectsListComponent} from '../components/project/projects-list.component';
import {ProjectsListInstructorComponent} from '../components/project/projects-list-instructor.component';
import {ProjectEditComponent} from '../components/project/project-edit.component';
import {ProjectAddComponent} from '../components/project/project-add.component';
//Modules
//Pipes
//Routings
import {AppRouting} from '../app.routing';
//Services
import {ProjectService} from '../services/project.service';
//Guards


@NgModule({
    // directives, components, and pipes
    declarations: [
        ProjectsListComponent,
        ProjectAddComponent,
        ProjectEditComponent,
        ProjectsListInstructorComponent
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
        ProjectService
    ],
    exports: [
        ProjectsListComponent,
        ProjectsListInstructorComponent
    ]
})
export class ProjectModule { }