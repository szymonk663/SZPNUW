export class ChangePasswordsModel {
    constructor(
        public id: number,
        public oldPassword: string,
        public newPassword: string
    ) { }
}