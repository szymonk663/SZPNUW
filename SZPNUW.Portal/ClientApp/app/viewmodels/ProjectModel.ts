export class ProjectModel {
    constructor(
        public Id: number,
        public Topic: string,
        public Description: string,
        public UserId: number | null,
        public SubjectId: number,
        public Active: boolean | null
    ) { }
}