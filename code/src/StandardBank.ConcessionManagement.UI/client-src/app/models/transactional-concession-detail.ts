import { BaseConcessionDetail } from "./base-concession-detail";

export class TransactionalConcessionDetail extends BaseConcessionDetail {
    transactionalConcessionDetailId: number;
    transactionType: string;
    transactionTypeId: number;
    volume: number;
    value: number;
    adValorem: number;
    fee: number;
	transactionTableNumberId: number;
    loadedPrice: string;
    approvedPrice: number;
	approvedTransactionTableNumberId: number;
	loadedTransactionTableNumberId: number;
    loadedTableNumber: string;
    approvedTableNumber: string;
    displayTextLoaded: string;
    displayTextApproved: string;
}
