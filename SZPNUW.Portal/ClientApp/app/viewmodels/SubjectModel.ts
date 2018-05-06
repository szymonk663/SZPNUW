export class SubjectModel {
    constructor(
        public id: number,
        public name: string,
        public description: string,
        public id_manager: number | null,
        public id_semester: number | null
    ) { }
}