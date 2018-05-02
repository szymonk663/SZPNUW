export class StudentModel {
    constructor(
        public id: number,
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