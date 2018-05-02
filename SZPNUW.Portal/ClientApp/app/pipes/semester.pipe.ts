import { Pipe, PipeTransform } from '@angular/core';
import { SemesterModel } from '../viewmodels/SemesterModel';

@Pipe({ name: 'departmentFilter' })
export class DepartmentFilterPipe implements PipeTransform {
    transform(allSemesters: SemesterModel[], department: string): SemesterModel[] | null {
        if (allSemesters == null)
            return null;
        else
            return allSemesters.filter(semester => semester.department.includes(department));
    }
}

@Pipe({ name: 'yearFilter' })
export class YearFilterPipe implements PipeTransform {
    transform(allSemesters: SemesterModel[], year: string): SemesterModel[] | null {
        if (allSemesters == null)
            return null;
        else
            return allSemesters.filter(x => x.year.includes(year));
    }
}