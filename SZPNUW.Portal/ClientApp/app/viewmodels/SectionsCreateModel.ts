export class SectionsCreateModel {
    constructor(
        public semesterId: number | null,
        public subjectId: number | null,
        public count: number
    ) { }
}