import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import 'rxjs/Rx';

//Components
import { SemesterComponent } from '../components/semester/semester.component';
import { SemesterDetailComponent } from '../components/semester/semester-detail.component';
//Modules
//Pipes
import { DepartmentFilterPipe, YearFilterPipe } from '../pipes/semester.pipe';
//Routings
import { AppRouting } from '../app.routing';
//Services
import { SemesterService } from '../services/semester.service';


@NgModule({
    // directives, components, and pipes
    declarations: [
        SemesterComponent,
        SemesterDetailComponent,
        DepartmentFilterPipe,
        YearFilterPipe
    ],
    // modules
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting
    ],
    // providers
    providers: [
        SemesterService
    ],
    exports: [
        //SemesterComponent,
        DepartmentFilterPipe,
        YearFilterPipe
    ]
})
export class SemesterModule { }