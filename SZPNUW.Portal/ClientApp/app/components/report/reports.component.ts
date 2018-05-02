import {Component, OnInit} from "@angular/core";
import {Router, ActivatedRoute, Params } from '@angular/router';
import {ReportService} from '../../services/report.service';
import {SectionService} from '../../services/section.service';
import {AccountService} from '../../services/account.service';
import {ReportModel} from '../../viewmodels/ReportModel';

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
    permission: string;
    storred: boolean = false;

    constructor(private route: ActivatedRoute,
        private reportService: ReportService,
        private sectionService: SectionService,
        private accountService: AccountService,
        private router: Router
    ) { }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            this.sectionId = +params['id'];
            if (this.sectionId)
                this.onRefresh();
        });
        this.getPermission();
        if (this.permission == 'u')
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
        this.reportService.downloadReport(this.selectedReport.id);
    }

    getPermission() {
        const item = localStorage.getItem('currentUser')
        if (item !== null) {
            this.permission = JSON.parse(item).permissions;
        }
    }

    onCheck() {
        const item = this.accountService.getUserId();
        if(item !== null)
        this.sectionService
            .studentInSection(this.sectionId, item)
            .then(result => this.storred = result, error => this.error = error);
    }

    onDelete() {
        this.onClear();
        this.reportService
            .deleteReport(this.selectedReport.id).then(response => {
                this.message = 'Plik został usunięty';
                this.onRefresh();
        }, error => this.error = error);
    }

    goBack() {
        if (this.permission == 'a')
            this.router.navigate(['/sections']);
        else if (this.permission == 'u')
            this.router.navigate(['/student/sections']);
    }

    onClear() {
        this.error = '';
        this.message = '';
    }
}