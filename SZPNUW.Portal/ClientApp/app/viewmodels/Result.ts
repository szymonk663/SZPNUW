export class Result {
    constructor(
        public Id: number | null,
        public IsSucceeded: boolean,
        public ErrorMessages: string,
        public PortalMessages: string
    ) { }
}