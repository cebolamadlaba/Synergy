import { Pipe, PipeTransform } from '@angular/core';
import { LendingConcession } from "../models/lending-concession";

@Pipe({
    name: 'lendingConcessionFilter'
})
export class LendingConcessionFilterPipe implements PipeTransform {

    transform(items: LendingConcession[], filterConcessionId): any {
        console.log('filterConcessionId', filterConcessionId);

        return filterConcessionId
            ? items.filter(item => item.concession.referenceNumber.indexOf(filterConcessionId) !== -1)
            : items;
    }

}
