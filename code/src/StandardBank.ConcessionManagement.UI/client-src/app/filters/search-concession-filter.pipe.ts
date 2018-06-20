import { Pipe, PipeTransform } from '@angular/core';
import { SearchConcessionDetail } from '../models/search-concession-detail';

@Pipe({
    name: 'searchConcessionFilter'
})
export class SearchConcessionFilterPipe implements PipeTransform {

    transform(items: SearchConcessionDetail[], concessionId): any {
        return concessionId
            ? items.filter(item => item.referenceNumber.indexOf(concessionId) !== -1)
            : items;
    }

}
