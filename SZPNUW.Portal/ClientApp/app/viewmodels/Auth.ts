export class Auth {
    constructor(
        public Id: number | null,
        public PId: number | null,
        public UserType: number | null,
        public IsSucceeded: boolean,
        public ErrorMessages: string,
        public PortalMessages: string
    ) { }
}