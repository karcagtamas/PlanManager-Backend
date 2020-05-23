import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IConfirmData } from '../interfaces';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmModalComponent } from '../components/common/confirm-modal/confirm-modal.component';
// import { ConfirmDialogComponent } from '../components/confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private snackBar: MatSnackBar, private dialog: MatDialog) {}

  /**
   * Error snackbar
   * @param {string} value Text to display
   * @memberof NotificationService
   */
  public error(value: string): void {
    this.snackBar.open(value, 'OK', {
      duration: 0,
      panelClass: ['error-snackbar'],
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }

  /**
   * Warning snackbar
   * @param {string} value Text to display
   * @memberof NotificationService
   */
  public warning(value: string): void {
    this.snackBar.open(value, 'OK', {
      duration: 5000,
      panelClass: ['warning-snackbar'],
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }

  /**
   * Success snackbar
   * @param {string} value Text to display
   * @memberof NotificationService
   */
  public success(value: string): void {
    this.snackBar.open(value, 'OK', {
      duration: 5000,
      panelClass: ['success-snackbar'],
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }

  /**
   * Confirm dialog
   * @param {IConfirmData} value Confirm panel's uniqe values
   * @returns {Promise<boolean>} Confirm or not (yes or no)
   * @memberof NotificationService
   */
  public confirm(value: IConfirmData): Promise<boolean> {
    return new Promise(resolve => {
      const dialogRef = this.dialog.open(ConfirmModalComponent, {
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
