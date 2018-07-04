export class SectionModel {
    constructor(
        public Id: number,
        public SectionNumber: number,
        public SubjectSemesterId: number,
        public ProjectId: number | null
    ) { }
}