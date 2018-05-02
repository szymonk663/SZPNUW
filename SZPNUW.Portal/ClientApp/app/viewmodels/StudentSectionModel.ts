export class StudentSectionModel {
    constructor(
        public id: number,
        public sectionId: number,
        public studentId: number | null,
        public rate?: number | null,
        public date?: Date | null
    ) { }
}