import { Role } from "./role";
import { Centre } from "./centre";
import { Region } from "./region";

export class User {
    id: number;
    aNumber: string;
    emailAddress: string;
    firstName: string;
    surname: string;
    fullName: string;
    isActive: boolean;
    userRoles: Role[];
    userRegions: Region[];
    selectedRegion: Region;
    userCentres: Centre[];
    selectedCentre: Centre;
    canRequest: boolean;
    canBcmApprove: boolean;
    canPcmApprove: boolean;
    isHO: boolean;
    isAdminAssistant: boolean;
    contactNumber: string;
    accountExecutiveUserId: number;
}
