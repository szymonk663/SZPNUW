import { Component, Input} from '@angular/core';

import { Instructor } from '../../viewmodels/Instructor';


@Component({
    selector: 'instructor-detail',
    templateUrl: '/template/instructor/instructor-detail.component.html',
})

export class InstructorDetailComponent {
    @Input()
    instructor: Instructor;
 
    constructor() { }

}