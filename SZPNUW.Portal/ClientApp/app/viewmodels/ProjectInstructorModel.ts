import { ProjectModel } from './ProjectModel';
import { InstructorModel } from './InstructorModel';


export class ProjectInstructorModel {
    constructor(
        public project: ProjectModel,
        public instructor: InstructorModel
    ) { }
}