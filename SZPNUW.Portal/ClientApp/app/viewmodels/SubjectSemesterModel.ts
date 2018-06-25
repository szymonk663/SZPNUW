export class SubjectSemesterModel {
    constructor(
        public Id: number,
        public SubjectId: number,
        public SemesterId: number | null
    ) { }
}