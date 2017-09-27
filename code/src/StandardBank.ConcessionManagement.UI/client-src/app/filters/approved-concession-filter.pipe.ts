import { Pipe, PipeTransform } from '@angular/core';
import { InboxConcession } from "../models/inbox-concession";

@Pipe({
    name: 'approvedConcessionFilter'
})
export class ApprovedConcessionFilterPipe implements PipeTransform {

    transform(items: InboxConcession[], concessionIdOrRiskGroupNumber): any {
        return concessionIdOrRiskGroupNumber
            ? items.filter(item => item.referenceNumber.indexOf(concessionIdOrRiskGroupNumber) !== -1 || String(item.riskGroupNumber).indexOf(concessionIdOrRiskGroupNumber) !== -1)
            : items;
    }

}
