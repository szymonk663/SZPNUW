export class SemesterModel {
    constructor(
        public Id: number,
        public Year: string,
        public SemesterNumber: number | null,
        public Department: string
    ) { }
}