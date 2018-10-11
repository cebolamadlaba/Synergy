import { Pipe, PipeTransform } from '@angular/core';
import { ConcessionComment } from "../models/concession-comment";

@Pipe({
  name: 'commentsFilter'
})
export class CommentsFilterPipe implements PipeTransform {

    transform(items: ConcessionComment[], commenttext): any {
        return commenttext
            ? items.filter(item => item.comment != null && item.comment.indexOf(commenttext) == -1)
            : items; 
    }
}
