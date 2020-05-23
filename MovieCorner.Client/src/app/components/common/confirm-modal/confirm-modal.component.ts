import { Component, OnInit, Inject } from '@angular/core';
import { IConfirmData } from 'src/app/interfaces';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

/**
 * Confirm Modal Componenet
 * Modal for the confirmation
 * Universal with paramters title and text
 * @export
 * @class ConfirmModalComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.scss']
})
export class ConfirmModalComponent implements OnInit {
  title = '';
  text = '';

  /**
   * Creates an instance of ConfirmModalComponent.
   * @param {IConfirmData} data
   * @param {MatDialogRef<ConfirmModalComponent>} dialogRef
   * @memberof ConfirmModalComponent
   */
  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: IConfirmData,
    public dialogRef: MatDialogRef<ConfirmModalComponent>
  ) {}

  /**
   * On Init hook
   * Create display texts
   * @memberof ConfirmModalComponent
   */
  ngOnInit() {
    this.title = `Delete ${this.data.title}`;
    this.text = `Are you sure you want to delete ${this.data.name}?`;
  }

  /**
   * Exits from the modal
   * @param {boolean} value Return value
   * @memberof ConfirmModalComponent
   */
  public exit(value: boolean): void {
    this.dialogRef.close(value);
  }
}
