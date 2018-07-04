export class MeetingModel {
    constructor(
        public Id: number,
        public SectionStudentId: number,
        public Date: Date,
        public Rate?: number | null
    ) { }
}