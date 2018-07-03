export class InstructorModel {
    constructor(
        public Id: number | null,
        public UserId: number | null,
        public Login: string,
        public Password: string,
        public FirstName: string,
        public LastName: string,
        public PESEL: string,
        public DateOfBirth: Date | null,
        public City: string,
        public Address: string,
        public Code: string,
    ) { }
}