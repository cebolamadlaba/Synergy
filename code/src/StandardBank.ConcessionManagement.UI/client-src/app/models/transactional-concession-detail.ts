import { BaseConcessionDetail } from "./base-concession-detail";

export class TransactionalConcessionDetail extends BaseConcessionDetail {
    transactionalConcessionDetailId: number;
    transactionType: string;
    transactionTypeId: number;
    volume: number;
    value: number;
    adValorem: number;
    baseRate: number;
    tableNumberId: number;
    loadedPrice: number;
    approvedPrice: number;
    approvedTableNumberId: number;
    loadedTableNumberId: number;
    loadedTableNumber: string;
    approvedTableNumber: string;
}
