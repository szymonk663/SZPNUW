import { ProjectModel } from './ProjectModel';
import { InstructorModel } from './InstructorModel';


export class ProjectInstructorModel {
    constructor(
        public Project: ProjectModel,
        public Instructor: InstructorModel
    ) { }
}