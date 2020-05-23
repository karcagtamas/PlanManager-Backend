import { Pipe, PipeTransform } from '@angular/core';

/**
 * Description Pipe
 * @export
 * @class DescriptionPipe
 * @implements {PipeTransform}
 */
@Pipe({
  name: 'description'
})
export class DescriptionPipe implements PipeTransform {
  /**
   * If the description value is exist return with the value else return with a default text.
   * @param {(string | null)} value Input description value
   * @returns {string} Result text value
   * @memberof DescriptionPipe
   */
  public transform(value: string | null): string {
    if (value === null || value === '') {
      return 'There is not any description';
    } else {
      return value;
    }
  }
}
