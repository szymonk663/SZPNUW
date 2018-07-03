export class RegistrationModel {
    constructor(
        public Login: string,
        public Password: string,
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