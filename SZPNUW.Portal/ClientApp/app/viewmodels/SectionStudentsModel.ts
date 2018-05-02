import { SectionModel } from './SectionModel';
import { StudentModel } from './StudentModel';


export class SectionStudentsModel {
    constructor(
        public section: SectionModel,
        public students: StudentModel[]
    ) { }
}