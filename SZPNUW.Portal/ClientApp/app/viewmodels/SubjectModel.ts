export class SubjectModel {
    constructor(
        public Id: number,
        public Name: string,
        public Description: string,
        public LeaderId: number | null,
        public SemesterId: number | null
    ) { }
}