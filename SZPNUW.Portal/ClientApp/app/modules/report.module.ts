import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpModule} from '@angular/http';
import {RouterModule}  from '@angular/router';
import {FormsModule} from '@angular/forms';
import 'rxjs/Rx';

//Components
import {ReportComponent} from '../components/report/report.component';
import {ReportsComponent} from '../components/report/reports.component';
//Modules
//Pipes
//Routings
import {AppRouting} from '../app.routing';
//Services
import {ReportService} from '../services/report.service';
//Guards


@NgModule({
    // directives, components, and pipes
    declarations: [
        ReportComponent,
        ReportsComponent
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
        ReportService
    ],
    exports: [
        ReportsComponent
    ]
})
export class ReportModule { }