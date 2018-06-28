export class SemestersIdModel {
    constructor(
        public StudentId: number,
        public SemesterId: number | null,
        public NewSemesterId: number | null
    ) { }
}