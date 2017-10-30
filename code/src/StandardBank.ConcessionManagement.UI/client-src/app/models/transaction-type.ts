import { TransactionTableNumber } from "./transaction-table-number";

export class TransactionType {
    id: number;
    concessionTypeId: number;
    concessionType: string;
	description: string;
    transactionTableNumbers: TransactionTableNumber[];
    importFileProductId: string;
}
