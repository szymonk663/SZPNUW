export class InstructorModel {
    constructor(
        public Id: number,
        public UserId: number,
        public Login: number,
        public FirstName: string,
        public LastName: string,
        public PESEL: string,
        public DateOfBirth: Date | null,
        public City: string,
        public Address: string
    ) { }
}