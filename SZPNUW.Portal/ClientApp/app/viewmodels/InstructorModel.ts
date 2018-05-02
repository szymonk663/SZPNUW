export class InstructorModel {
    constructor(
        public id: number,
        public firstName: string,
        public lastName: string,
        public pesel: string,
        public birthday: Date | null,
        public city: string,
        public addres: string
    ) { }
}