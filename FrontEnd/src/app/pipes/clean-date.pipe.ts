import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cleanDate'
})
export class CleanDatePipe implements PipeTransform {

  transform(value: string, ...args: unknown[]): unknown {
    var retVal = '';
    retVal += value.slice(8, 10) + '-' + value.slice(5,7) + '-' + value.slice(0, 4);
    return retVal;
  }

}
