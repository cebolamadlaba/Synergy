import { Role } from "./role";

export class User {
    id: number;
    aNumber: string;
    emailAddress: string;
    firstName: string;
    surname: string;
    isActive: boolean;
    userRoles: Role[];
}
