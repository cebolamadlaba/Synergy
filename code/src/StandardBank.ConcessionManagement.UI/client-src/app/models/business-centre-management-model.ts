import { User } from "./user";

export class BusinessCentreManagementModel {
    centreId: number;
    centreName: string;
    isActive: boolean;
    businessCentreManagerId: number;
    businessCentreManager: string;
    regionId: number;
    region: string;
    requestorCount: number;
    accountExecutives: User[];
}
