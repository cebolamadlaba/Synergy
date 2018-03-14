import { Pipe, PipeTransform } from '@angular/core';
import { ApprovedConcession } from "../models/approved-concession";

@Pipe({
    name: 'searchConcessionFilter'
})
export class SearchConcessionFilterPipe implements PipeTransform {

    transform(items: ApprovedConcession[], customerNameOrRiskGroupNumber): any {
        return customerNameOrRiskGroupNumber
            ? items.filter(item => item.customerName.indexOf(customerNameOrRiskGroupNumber) !== -1 || String(item.riskGroupNumber).indexOf(customerNameOrRiskGroupNumber) !== -1)
            : items;
    }

}
