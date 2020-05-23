import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {
  /**
   * Creates an instance of UtilsService.
   * @memberof UtilsService
   */
  constructor() {}

  /**
   * Returns with a required message for forms
   * @returns {string} Message text
   * @memberof UtilsService
   */
  public isRequiredMessage(): string {
    return 'The field is required';
  }

  /**
   * Returns with a max length message for forms
   * @param {number} max Max length value
   * @returns {string} Message text
   * @memberof UtilsService
   */
  public maxLengthMessage(max: number): string {
    return `The field's maximum length is ${max}`;
  }

  /**
   * Returns with a min length message for forms
   * @param {number} min Min length value
   * @returns {string} Message text
   * @memberof UtilsService
   */
  public minLengthMessage(min: number): string {
    return `The field's minimum length is ${min}`;
  }

  /**
   * Returns with a min value message for forms
   * @param {number} max Max value
   * @returns {string} Message text
   * @memberof UtilsService
   */
  public maxMessage(max: number): string {
    return `The field's maximum value is ${max}`;
  }

  /**
   * Returns with a min value message for forms
   * @param {number} min Min value
   * @returns {string} Message text
   * @memberof UtilsService
   */
  public minMessage(min: number): string {
    return `The field's minimum value is ${min}`;
  }

  /**
   * Returns with a email message for forms
   * @returns {string} Message text
   * @memberof UtilsService
   */
  public emailMessage(): string {
    return `The e-mail format is invalid`;
  }

  /**
   * Returns with a form is invalid message
   * @returns {string} Message text
   * @memberof UtilsService
   */
  public formIsInvalidMessage(): string {
    return 'The datas are invalid';
  }

  public invalidLengthIntervall(minimum: number, current: number): string {
    return `Invalid length intervall. The length must be ${minimum}, but now is ${current}`;
  }

  /**
   * Checks the form has a given error for a given input
   * @param {FormGroup} form The checked form
   * @param {string} controlName The requested input's control name
   * @param {string} errorName The requested error's name
   * @returns {boolean} The error is exist or not
   * @memberof UtilsService
   */
  public hasError(
    form: FormGroup,
    controlName: string,
    errorName: string
  ): boolean {
    if (!form) {
      return true;
    }
    return (
      form.get(controlName).hasError(errorName) && form.get(controlName).touched
    );
  }

  /**
   * Date to string version
   * Format will be yyyy-mm-dd
   * @param {Date} date Date to convert
   * @returns {string} Converted date string
   * @memberof UtilsService
   */
  public toDateString(date: Date): string {
    const year: number = date.getFullYear();
    const month: number = date.getMonth() + 1;
    const day: number = date.getDate();

    const monthString: string = month >= 10 ? month.toString() : `0${month}`;
    const dayString: string = day >= 10 ? day.toString() : `0${day}`;

    return `${year}-${monthString}-${dayString}`;
  }
}
