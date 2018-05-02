import {Component, OnChanges, Input} from "@angular/core";
import {SectionService} from "../../services/section.service";
import {SectionsCreateModel} from '../../viewmodels/SectionsCreateModel';
import {SectionsListComponent} from './sections-list.component';

@Component({
    selector: "sections-form",
    templateUrl: "./sections-form.component.html"
})

export class SectionsFormComponent implements OnChanges {
    @Input()
    subjectId: number;
    @Input()
    semesterId: number;
    sectionsCreator: SectionsCreateModel = new SectionsCreateModel(null, null, 1);
    error: string = '';
    message: string = '';

    constructor(private sectionService: SectionService,
        private sectionsListComponent: SectionsListComponent
    ) { }

    ngOnChanges() {
        this.onClear();
        this.sectionsCreator.semesterId = this.semesterId;
        this.sectionsCreator.subjectId = this.subjectId;
    }

    onSubmit() {
        this.sectionService
            .addNewSections(this.sectionsCreator)
            .then(result => {
                this.message = result
                this.sectionsCreator.count = 1;
                this.sectionsListComponent.onRefresh();
            }, error => this.error = error);
    }

    onClear() {
        this.error = '';
        this.message = '';
    }
}