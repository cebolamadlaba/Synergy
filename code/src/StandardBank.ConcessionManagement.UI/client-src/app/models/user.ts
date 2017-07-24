import { Role } from "./role";
import { Centre } from "./centre";
import { Region } from "./region";

export class User {
    id: number;
    aNumber: string;
    emailAddress: string;
    firstName: string;
    surname: string;
    isActive: boolean;
    userRoles: Role[];
    userRegions: Region[];
    userCentres: Centre[];
}
