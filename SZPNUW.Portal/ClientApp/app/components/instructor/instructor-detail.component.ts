import { Component, Input} from '@angular/core';

import { InstructorModel } from '../../viewmodels/InstructorModel';


@Component({
    selector: 'instructor-detail',
    templateUrl: './instructor-detail.component.html',
})

export class InstructorDetailComponent {
    @Input()
    instructor: InstructorModel;
 
    constructor() { }

}