export class SemestersIdModel {
    constructor(
        public id_student: number,
        public id_semester: number | null,
        public id_semesterNew: number | null
    ) { }
}