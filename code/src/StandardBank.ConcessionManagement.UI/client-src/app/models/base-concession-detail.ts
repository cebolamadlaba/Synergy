export class BaseConcessionDetail {
    concessionDetailId: number;
    concessionId: number;
    customerName: string;
    accountNumber: string;
    legalEntityId: number;
    legalEntityAccountId: number;
    expiryDate: Date;
    dateApproved: Date;
	isMismatched: boolean;
	isExpired: boolean;
    isExpiring: boolean;
    priceExported: boolean;
    priceExportedDate: Date;
}
