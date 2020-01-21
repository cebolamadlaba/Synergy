import { CashConcession } from "../models/cash-concession";

export class CashConcessionBaseService {

    validationError: String[];

    addValidationError(validationDetail) {
        if (!this.validationError)
            this.validationError = [];

        if (!this.validationError.includes(validationDetail)) {
            this.validationError.push(validationDetail);
        }
    }

    checkConcessionExpiryDate(cashConcession: CashConcession) {
        if (cashConcession.cashConcessionDetails.length > 1) {
            var firstDate;
            cashConcession.cashConcessionDetails.forEach(concession => {
                if (!firstDate) {
                    firstDate = concession.expiryDate;
                } else if (firstDate.getTime() != concession.expiryDate.getTime()) {
                    this.addValidationError("All concessions must have the same expiry date.");
                }
            });
        }
    }
}
