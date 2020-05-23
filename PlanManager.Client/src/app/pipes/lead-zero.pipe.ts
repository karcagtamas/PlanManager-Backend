import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'leadZero'
})
export class LeadZeroPipe implements PipeTransform {
  transform(value: number): string {
    if (value > 9) {
      return value.toString();
    } else {
      return `0${value}`;
    }
  }
}
