export class SectionsCreateModel {
    constructor(
        public SemesterId: number | null,
        public SubjectId: number | null,
        public Count: number
    ) { }
}