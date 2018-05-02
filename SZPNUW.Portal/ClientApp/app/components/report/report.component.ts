import {Component, ViewChild, Input} from "@angular/core";

import {ReportService} from '../../services/report.service';
import {ReportsComponent} from './reports.component';

@Component({
    selector: "report",
    templateUrl: "./report.component.html"
})

export class ReportComponent {
    @Input()
    sectionId: number;
    error: string = '';
    message: string = '';
    public file: File[];
    @ViewChild("fileInput") fileInput : any;
    
    constructor(private reportService: ReportService,
        private reportsComponent: ReportsComponent
    ) { }

    fileEvent(fileInput: any) {
        this.file = fileInput.target.files;
    }

    onSubmit(): void {
        this.onClear();
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0]
            this.reportService
                .uploadReport(fileToUpload, this.sectionId)
                .then(result => {
                    this.message = 'Plik został przesłany.'
                    this.reportsComponent.onRefresh();
                }, error => this.error = error);
        }
    }

    onClear() {
        this.error = '';
        this.message = '';
    }
}