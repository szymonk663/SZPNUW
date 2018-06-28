export class StudentModel {
    constructor(
        public Id: number,
        public FirstName: string,
        public LastName: string,
        public PESEL: string,
        public DateOfBirth: Date | null,
        public City: string,
        public Address: string,
        public Album: string,
        public SemesterId: number | null
    ) { }
}