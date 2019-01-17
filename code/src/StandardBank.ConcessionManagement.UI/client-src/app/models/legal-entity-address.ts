export class LegalEntityAddress {
    id: number;
    legalEntityId: number;
    contactPerson: string;
    customerName: string;
    postalAddress: string;
    city: string;
    postalCode: string;
    dateCreated: Date;
    datemodified: Date | null;
}
