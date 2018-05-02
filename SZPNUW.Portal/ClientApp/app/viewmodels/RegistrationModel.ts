export class RegistrationModel {
    constructor(
        public login: string,
        public password: string,
        public firstName: string,
        public lastName: string,
        public pesel: string,
        public birthday: Date | null,
        public city: string,
        public addres: string,
        public album: string,
        public id_sem: number | null
    ) { }
}