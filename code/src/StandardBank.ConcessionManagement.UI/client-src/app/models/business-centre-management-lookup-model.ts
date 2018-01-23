import { User } from "./user";
import { Region } from "./region";

export class BusinessCentreManagementLookupModel {
    businessCentreManagers: User[];
    accountExecutives: User[];
    regions: Region[];
}
