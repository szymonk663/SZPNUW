import { ModuleWithProviders } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AdminGuard } from "./guards/admin.guard";
import { LecturerGuard } from "./guards/lecturer.guard";
import { StudentGuard } from "./guards/student.guard";

//Components
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { RegistrationComponent } from './components/account/registration.component';
import { LoginComponent } from './components/account/login.component';
import { InstructorsComponent } from "./components/instructor/instructors.component";
import { InstructorRegistrationComponent } from "./components/account/instructor-registration.component";
import { InstructorComponent } from "./components/instructor/instructor.component";
import { InstructorFormComponent } from "./components/instructor/instructor-form.component";
import { InstructorPasswordComponent } from "./components/instructor/instructor-password.component";
import { SemesterComponent } from "./components/semester/semester.component";
import { SemesterDetailComponent } from "./components/semester/semester-detail.component";
import { SubjectsComponent } from "./components/subject/subjects.component";
import { SubjectComponent } from "./components/subject/subject.component";
import { SubjectDetailComponent } from "./components/subject/subject-detail.component";
import { SubjectFormComponent } from "./components/subject/subject-form.component";
import { SubjectSemesterComponent } from "./components/subject/subject-semester.component";
import { StudentListComponent } from "./components/student/student-list.component";
import { StudentDetailComponent } from "./components/student/student-detail.component";
import { ProjectsListInstructorComponent } from "./components/project/projects-list-instructor.component";
import { SectionsComponent } from "./components/section/sections.component";
import { SectionsStudentComponent } from "./components/section/sections-student.component";
import { ReportsComponent } from "./components/report/reports.component";
import { StudentComponent } from "./components/account/student/student.component";
import { StudentProfileDetailComponent } from "./components/account/student/student-profile-detail.component";
import { StudentPasswordComponent } from "./components/account/student/student-password.component";
import { AdminListComponent } from "./components/account/admin/admin-list.component";
import { AdminRegistrationComponent } from "./components/account/admin/admin-registration.component";
import { AdminComponent } from './components/account/admin/admin.component';
import { AdminFormComponent } from './components/account/admin/admin-form.component';
import { AdminPasswordComponent } from './components/account/admin/admin-password.component';
import { SysLogComponent } from './components/syslog/syslogs.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'counter',
        component: CounterComponent
    },
    {
        path: 'fetch-data',
        component: FetchDataComponent
    },
    {
        path: 'registration',
        component: RegistrationComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'instructors',
        component: InstructorsComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'instructors/registration',
        component: InstructorRegistrationComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'instructor',
        component: InstructorComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'instructor/detail',
        component: InstructorFormComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'instructor/password',
        component: InstructorPasswordComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'semester',
        component: SemesterComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'semester/detail',
        component: SemesterDetailComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'semester/detail/:id',
        component: SemesterDetailComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'subjects',
        component: SubjectsComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'subject/:id',
        component: SubjectComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'subject/detail/:id',
        component: SubjectDetailComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'newsubject',
        component: SubjectFormComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'subject/semester/:id_subject/:id_semester',
        component: SubjectSemesterComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'subject/semester/:id_subject',
        component: SubjectSemesterComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'students',
        component: StudentListComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'student/detail/:id',
        component: StudentDetailComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'projects',
        component: ProjectsListInstructorComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'sections',
        component: SectionsComponent,
        canActivate: [LecturerGuard]
    },
    {
        path: 'student/sections',
        component: SectionsStudentComponent,
        canActivate: [StudentGuard]
    },
    {
        path: 'report/section/:id',
        component: ReportsComponent
    },
    {
        path: 'student',
        component: StudentComponent,
        canActivate: [StudentGuard]
    },
    {
        path: 'student/detail',
        component: StudentProfileDetailComponent,
        canActivate: [StudentGuard]
    },
    {
        path: 'student/password',
        component: StudentPasswordComponent,
        canActivate: [StudentGuard]
    }, 
    {
        path: 'admins/registration',
        component: AdminRegistrationComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'admins',
        component: AdminListComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'admin',
        component: AdminComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'admin/edit',
        component: AdminFormComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'admin/password',
        component: AdminPasswordComponent,
        canActivate: [AdminGuard]
    },
    {
        path: 'syslogs',
        component: SysLogComponent,
        canActivate: [AdminGuard]
    },
    {
        path: '**',
        redirectTo: 'home'
    },
];

export const AppRoutingProviders: any[] = [
];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(routes);  