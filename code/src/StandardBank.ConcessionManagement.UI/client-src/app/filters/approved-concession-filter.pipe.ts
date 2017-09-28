import { Pipe, PipeTransform } from '@angular/core';
import { ApprovedConcession } from "../models/approved-concession";

@Pipe({
    name: 'approvedConcessionFilter'
})
export class ApprovedConcessionFilterPipe implements PipeTransform {

    transform(items: ApprovedConcession[], concessionIdOrRiskGroupNumber): any {
        return concessionIdOrRiskGroupNumber
            ? items.filter(item => item.referenceNumber.indexOf(concessionIdOrRiskGroupNumber) !== -1 || String(item.riskGroupNumber).indexOf(concessionIdOrRiskGroupNumber) !== -1)
            : items;
    }

}
