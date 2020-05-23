import { Pipe, PipeTransform } from '@angular/core';

/**
 * Seen Pipe
 * @export
 * @class SeenPipe
 * @implements {PipeTransform}
 */
@Pipe({
  name: 'seen'
})
export class SeenPipe implements PipeTransform {
  /**
   * Returns with a visibility icon name, in spite of the input logic value
   * @param {boolean} value Logical value. Seen or not
   * @returns {string} Icon's string value
   * @memberof SeenPipe
   */
  public transform(value: boolean): string {
    if (value) {
      return 'visibility';
    } else {
      return 'visibility_off';
    }
  }
}
