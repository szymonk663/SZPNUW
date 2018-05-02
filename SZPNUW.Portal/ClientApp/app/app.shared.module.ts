import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

//Components
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { RegistrationComponent } from './components/account/registration.component';

//Modules
import { AccountModule } from './modules/account.module';
import { SemesterModule } from './modules/semester.module';
import { SubjectModule } from './modules/subject.module';
import { SectionModule } from './modules/section.module';
import { ReportModule } from './modules/report.module';
//Services
import { AccountService } from './services/account.service';
//Routings
import { AppRouting } from './app.routing';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        RegistrationComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting,
        AccountModule,
        SemesterModule,
        SubjectModule,
        SectionModule,
        ReportModule
    ],
    providers: [
        AccountService
    ]
})
export class AppModuleShared {
}
