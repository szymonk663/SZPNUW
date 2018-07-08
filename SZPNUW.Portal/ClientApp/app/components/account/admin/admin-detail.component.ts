import { Component, Input} from '@angular/core';

import { UserModel } from '../../../viewmodels/UserModel';


@Component({
    selector: 'admin-detail',
    templateUrl: './admin-detail.component.html',
})

export class AdminDetailComponent {
    @Input()
    user: UserModel;
 
    constructor() { }

}