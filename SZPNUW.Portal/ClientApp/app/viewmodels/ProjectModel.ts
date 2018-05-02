export class ProjectModel {
    constructor(
        public id: number,
        public topic: string,
        public description: string,
        public id_instructor: number | null,
        public id_subject: number,
        public active: boolean | null
    ) { }
}