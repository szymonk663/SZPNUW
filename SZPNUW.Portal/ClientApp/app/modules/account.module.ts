import { NgModule } from '@angular/core';
import 'rxjs/Rx';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
//Components
import { LoginComponent } from '../components/account/login.component';
import { StudentComponent } from '../components/account/student/student.component';
import { StudentProfileComponent } from '../components/account/student/student-profile.component';
import { StudentProfileDetailComponent } from '../components/account/student/student-profile-detail.component';
import { StudentPasswordComponent } from '../components/account/student/student-password.component';
import { StudentListComponent } from '../components/student/student-list.component';
import { StudentsSemesterComponent } from '../components/student/students-semester.component';
import { StudentDetailComponent } from '../components/student/student-detail.component';
import { StudentSemesterComponent } from '../components/student/student-semester.component';
import { InstructorsComponent } from '../components/instructor/instructors.component';
import { InstructorRegistrationComponent } from '../components/account/instructor-registration.component';
import { InstructorDetailComponent } from '../components/instructor/instructor-detail.component';
import { InstructorComponent } from '../components/instructor/instructor.component';
import { InstructorFormComponent } from '../components/instructor/instructor-form.component';
import { InstructorPasswordComponent } from '../components/instructor/instructor-password.component';
//Modules
import { SemesterModule } from './semester.module';
//Pipes
import { LastNameFilterPipe } from '../pipes/instructor.pipe';
//Services
//Routings
import { AppRouting } from '../app.routing';
import { AccountService } from '../services/account.service';
//Guards
import { StudentGuard } from '../guards/student.guard'
import { LecturerGuard } from '../guards/lecturer.guard';
import { AdminGuard } from '../guards/admin.guard';

@NgModule({
    // directives, components, and pipes
    declarations: [
        LoginComponent,
        StudentComponent,
        StudentProfileComponent,
        StudentProfileDetailComponent,
        StudentPasswordComponent,
        StudentListComponent,
        StudentsSemesterComponent,
        StudentDetailComponent,
        StudentSemesterComponent,
        InstructorsComponent,
        InstructorRegistrationComponent,
        InstructorDetailComponent,
        InstructorComponent,
        InstructorFormComponent,
        InstructorPasswordComponent,
        LastNameFilterPipe
    ],
    // providers
    providers: [
        AccountService,
        StudentGuard,
        AdminGuard,
        LecturerGuard
    ],
    // modules
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting,
        SemesterModule
    ],
    exports: [
        StudentComponent,
        StudentListComponent,
        InstructorComponent,
        LastNameFilterPipe
    ]
})
export class AccountModule { }