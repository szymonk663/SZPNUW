import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router"
import {SemesterService} from "../../services/semester.service";
import {SemesterModel} from "../../viewmodels/SemesterModel";


@Component({
    selector: "semesetr",
    templateUrl: "./semester.component.html"
})

export class SemesterComponent implements OnInit {
    departments: string[];
    department: string = '';
    years: string[];
    year: string = '';
    semesters: SemesterModel[];
    selectedSemester: SemesterModel;
    error: string = '';
    constructor(private semesterService: SemesterService, private router: Router) { }

    ngOnInit() {
        this.semesterService.getSemesters().then(semesters => this.semesters = semesters, error => this.error = error);
        this.semesterService.getDepartments().then(departments => this.departments = departments, error => this.error = error);
        this.semesterService.getYears().then(years => this.years = years, error => this.error = error);
    }

    onSelect(semester: SemesterModel): void {
        this.selectedSemester = semester;
    }

    add() {
        this.router.navigate(['/semester/detail']);
    }

    edit() {
        this.router.navigate(['/semester/detail', this.selectedSemester.Id]);
    }
}