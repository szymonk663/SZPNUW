import {Component, OnInit} from "@angular/core";
import {Router, ActivatedRoute, Params } from '@angular/router';
import {ReportService} from '../../services/report.service';
import {SectionService} from '../../services/section.service';
import {AccountService} from '../../services/account.service';
import {ReportModel} from '../../viewmodels/ReportModel';
import { AppComponent } from "../app/app.component";

@Component({
    selector: "reports",
    templateUrl: "./reports.component.html"
})

export class ReportsComponent implements OnInit {
    error: string = '';
    message: string = '';
    sectionId: number;
    reports: ReportModel[] | null;
    selectedReport: ReportModel;
    permission: number;
    storred: boolean = false;

    constructor(private route: ActivatedRoute,
        private reportService: ReportService,
        private sectionService: SectionService,
        private accountService: AccountService,
        private appComponent: AppComponent,
        private router: Router
    ) { }

    ngOnInit() {
        console.log(this.appComponent.auth);
        this.route.params.forEach((params: Params) => {
            this.sectionId = +params['id'];
            if (this.sectionId)
                this.onRefresh();
        });
        if (this.appComponent.auth && this.appComponent.auth.UserType == 1)
            this.onCheck();
    }

    onSelect(report: ReportModel) {
        this.selectedReport = report;
    }

    onRefresh() {
        this.onClear();
        this.reportService
            .getSectionReports(this.sectionId)
            .then(reports => this.reports = reports, error => {
                this.error = error;
                if (error = 'Brak sprawozdań.')
                    this.reports = null;
            });
    }

    onDownload() {
        this.reportService.downloadReport(this.selectedReport.Id);
    }

    onCheck() {
        const item = this.accountService.getPId();
        if(item !== null)
        this.sectionService
            .studentInSection(this.sectionId, item)
            .then(result => this.storred = result, error => this.error = error);
    }

    onDelete() {
        this.onClear();
        this.reportService
            .deleteReport(this.selectedReport.Id).then(response => {
                this.message = 'Plik został usunięty';
                this.onRefresh();
        }, error => this.error = error);
    }

    goBack() {
        if (this.appComponent.auth && this.appComponent.auth.UserType == 2)
            this.router.navigate(['/sections']);
        else if (this.appComponent.auth && this.appComponent.auth.UserType == 1)
            this.router.navigate(['/student/sections']);
    }

    onClear() {
        this.error = '';
        this.message = '';
    }
}