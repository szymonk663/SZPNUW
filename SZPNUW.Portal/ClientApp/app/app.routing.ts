import { ModuleWithProviders } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./guards/auth.guard";

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
        canActivate: [AuthGuard]
    },
    {
        path: 'instructors/registration',
        component: InstructorRegistrationComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'instructor',
        component: InstructorComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'instructor/detail',
        component: InstructorFormComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'instructor/password',
        component: InstructorPasswordComponent,
        canActivate: [AuthGuard]
    },
    {
        path: '**',
        redirectTo: 'home'
    },
];

export const AppRoutingProviders: any[] = [
];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(routes);  