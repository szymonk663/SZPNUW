import { SectionModel } from './SectionModel';
import { StudentModel } from './StudentModel';


export class SectionStudentsModel {
    constructor(
        public Section: SectionModel,
        public Students: StudentModel[]
    ) { }
}