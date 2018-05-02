export class MeetingModel {
    constructor(
        public id: number,
        public sectionStudentId: number,
        public date: Date | null,
        public rate?: number | null
    ) { }
}