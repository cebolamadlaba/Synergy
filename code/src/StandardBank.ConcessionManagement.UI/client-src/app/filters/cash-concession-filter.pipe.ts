import { Pipe, PipeTransform } from '@angular/core';
import { CashConcession } from "../models/cash-concession";

@Pipe({
    name: 'cashConcessionFilter'
})
export class CashConcessionFilterPipe implements PipeTransform {

    transform(items: CashConcession[], filterConcessionId): any {
        return filterConcessionId
            ? items.filter(item => item.concession.referenceNumber.indexOf(filterConcessionId) !== -1)
            : items;
    }

}
