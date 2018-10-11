export class ConcessionComment {
    id: number;
    concessionId: number;
    userId: number;
    userDescription: string;
    concessionSubStatusId: number;
    concessionSubStatusDescription: string;
    comment: string;
    systemDate: Date;
    hide: boolean;
}
