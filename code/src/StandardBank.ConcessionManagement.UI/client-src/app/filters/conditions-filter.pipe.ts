import { Pipe, PipeTransform } from '@angular/core';
import { ConcessionCondition } from "../models/concession-condition";

@Pipe({
  name: 'conditionsFilter'
})
export class ConditionsFilterPipe implements PipeTransform {

    transform(items: ConcessionCondition[], concessionIdOrRiskGroupNumber): any {
        return concessionIdOrRiskGroupNumber
            ? items.filter(item => item.concessionReferenceNumber.indexOf(concessionIdOrRiskGroupNumber) !== -1 || String(item.riskGroupNumber).indexOf(concessionIdOrRiskGroupNumber) !== -1)
            : items;
    }

}
