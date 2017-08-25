import { Pipe, PipeTransform } from '@angular/core';
import { BaseConcession } from "../models/base-concession";

@Pipe({
    name: 'baseConcessionFilter'
})
export class BaseConcessionFilterPipe implements PipeTransform {

    transform(items: BaseConcession[], filterConcessionId): any {
        return filterConcessionId
            ? items.filter(item => item.concession.referenceNumber.indexOf(filterConcessionId) !== -1)
            : items;
    }

}
