export class StudentSectionModel {
    constructor(
        public Id: number,
        public SectionId: number,
        public StudentId: number | null,
        public Rate?: number | null,
        public Date?: Date | null
    ) { }
}