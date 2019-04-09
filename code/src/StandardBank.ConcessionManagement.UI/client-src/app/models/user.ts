import { Role } from "./role";
import { Centre } from "./centre";
import { UserSubRole } from "./userSubRole";
import { Region } from "./region";

import { AccountExecutiveAssistant } from "./account-executive-assistant";

export class User {
    id: number;
    aNumber: string;
    emailAddress: string;
    firstName: string;
    surname: string;
    fullName: string;
    isActive: boolean;
    userRoles: Role[];
    userCentres: Centre[];
    userSubRole: UserSubRole[];
    selectedCentre: Centre;
    centreId: number;
    subRoleId: number;
    canRequest: boolean;
    canBcmApprove: boolean;
    canPcmApprove: boolean;
    canApprove: boolean;
    isRequestor: boolean;
    isHO: boolean;
    isPCM: boolean;
    isBCM: boolean;
    isAdminAssistant: boolean;
    contactNumber: string;
    accountExecutiveUserId: number;

    accountExecutives: AccountExecutiveAssistant[];

    validated: boolean;
    errorMessage: string;

}
