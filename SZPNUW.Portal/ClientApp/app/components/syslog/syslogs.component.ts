import {Component, OnInit, Input} from "@angular/core";
import {Router} from "@angular/router";

import {SysLogModel} from '../../viewmodels/SysLogModel';
import {SysLogService} from "../../services/syslog.service";

@Component({
    selector: "syslogs",
    templateUrl: "./syslogs.component.html"
})

export class SysLogComponent implements OnInit {
    public error = '';
    public sysLogs: SysLogModel[];
    public selectedSysLog: SysLogModel;

    constructor(private router: Router, private sysLogService: SysLogService) { }

    ngOnInit() {
        this.sysLogService.getSysLogs().then(
            sysLogs => this.sysLogs = sysLogs,
                reject => this.error = reject
        );
    }

    onSelect(sysLog: SysLogModel): void {
        this.selectedSysLog = sysLog;
    }
}