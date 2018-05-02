import { ProjectModel } from './ProjectModel';
import { SubjectModel } from './SubjectModel';


export class ProjectSubjectModel {
    constructor(
        public project: ProjectModel,
        public subject: SubjectModel
    ) { }
}