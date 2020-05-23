import { Injectable } from '@angular/core';
import { IConfirmData } from '../models';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmComponent } from '../components/confirm/confirm.component';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private dialog: MatDialog) {}

  /**
   * Confirm dialog
   * @param {IConfirmData} value Confirm panel's uniqe values
   * @returns {Promise<boolean>} Confirm or not (yes or no)
   * @memberof NotificationService
   */
  public confirm(value: IConfirmData): Promise<boolean> {
    return new Promise(resolve => {
      const dialogRef = this.dialog.open(ConfirmComponent, {
        data: value
      });

      dialogRef.afterClosed().subscribe((res: boolean) => {
        if (res !== undefined) {
          resolve(res);
        } else {
          resolve(false);
        }
      });
    });
  }
}
