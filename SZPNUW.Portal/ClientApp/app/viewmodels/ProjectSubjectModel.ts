import { ProjectModel } from './ProjectModel';
import { SubjectModel } from './SubjectModel';


export class ProjectSubjectModel {
    constructor(
        public Project: ProjectModel,
        public Subject: SubjectModel
    ) { }
}