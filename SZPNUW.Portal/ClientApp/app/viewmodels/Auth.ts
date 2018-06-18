export class Auth {
    constructor(
        public Id: number | null,
        public Permissions: string,
        public IsSucceeded: boolean,
        public ErrorMessages: string,
        public PortalMessages: string
    ) { }
}