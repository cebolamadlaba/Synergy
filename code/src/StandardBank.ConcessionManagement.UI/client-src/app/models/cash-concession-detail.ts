import { BaseConcessionDetail } from "./base-concession-detail";

export class CashConcessionDetail extends BaseConcessionDetail {
    cashConcessionDetailId: number;
    channel: string;
    channelTypeId: number;
    bpId: number;
    volume: number;
    value: number;
    loadedPrice: number;
    approvedPrice: number;
    baseRate: number;
    adValorem: number;
    accrualTypeId: number;
    tableNumberId: number;
    approvedTableNumberId: number;
    loadedTableNumberId: number;
    approvedTableNumber: string;
    loadedTableNumber: string;
}
