export class SemesterModel {
    constructor(
        public id: number,
        public year: string,
        public semesterNumber: number | null,
        public department: string
    ) { }
}