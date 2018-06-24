import { Pipe, PipeTransform } from '@angular/core';
import { InstructorModel } from '../viewmodels/InstructorModel';

@Pipe({ name: 'lastNameFilter' })
export class LastNameFilterPipe implements PipeTransform {
    transform(allInstructors: InstructorModel[], lastName: string): InstructorModel[] | null {
        if (allInstructors == null)
            return null;
        else
            return allInstructors.filter(instructor => instructor.LastName.includes(lastName));
    }
}
