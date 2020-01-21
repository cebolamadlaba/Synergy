import { TransactionalConcession } from "../models/transactional-concession";

export class TransactionalConcessionBaseService {

    validationError: String[];

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        if (!this.validationError.includes(validationDetail)) {
            this.validationError.push(validationDetail);
        }
    }

    checkConcessionExpiryDate(transactionalConcession: TransactionalConcession) {
        if (transactionalConcession.transactionalConcessionDetails.length > 1) {
            var firstDate;
            transactionalConcession.transactionalConcessionDetails.forEach(concession => {
                if (!firstDate) {
                    firstDate = concession.expiryDate;
                } else if (firstDate.getTime() != concession.expiryDate.getTime()) {
                    this.addValidationError("All concessions must have the same expiry date.");
                }
            });
        }
    }
}
