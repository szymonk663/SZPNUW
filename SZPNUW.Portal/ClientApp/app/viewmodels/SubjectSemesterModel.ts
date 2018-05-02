export class SubjectSemesterModel {
    constructor(
        public id: number,
        public id_subject: number,
        public id_semester: number | null
    ) { }
}