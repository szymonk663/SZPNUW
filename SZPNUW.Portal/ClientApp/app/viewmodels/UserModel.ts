
export class UserModel {
    constructor(
        public UserId: number | null,
        public Login: string,
        public Password: string,
        public FirstName: string,
        public LastName: string,
        public PESEL: string,
        public DateOfBirth: Date | null,
        public City: string,
        public Address: string,
    ) { }
}