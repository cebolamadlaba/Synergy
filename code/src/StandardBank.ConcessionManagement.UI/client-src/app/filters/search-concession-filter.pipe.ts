import { Pipe, PipeTransform } from '@angular/core';
import { SearchConcessionDetail } from '../models/search-concession-detail';

@Pipe({
    name: 'searchConcessionFilter'
})
export class SearchConcessionFilterPipe implements PipeTransform {

    transform(items: SearchConcessionDetail[], filterConcessionId): any {
        return filterConcessionId
            ? items.filter(item => item.referenceNumber.indexOf(filterConcessionId) !== -1)
            : items;
    }


  


}
