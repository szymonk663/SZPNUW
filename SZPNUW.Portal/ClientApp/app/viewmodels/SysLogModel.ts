export class SysLogModel {
    constructor(
        public Id: number,
        public Name: string,
        public Details: string | null,
        public Date: Date
    ) { }
}