import { Subject } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/services';

/**
 * Loader Componenet
 * Managing loader animation
 * @export
 * @class LoaderComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit {
  color = 'primary';
  mode = 'indeterminate';
  value = 50;
  isLoading: Subject<boolean> = this.loaderService.isLoading;

  /**
   * Creates an instance of LoaderComponent.
   * @param {LoaderService} loaderService
   * @memberof LoaderComponent
   */
  constructor(private loaderService: LoaderService) {}

  /**
   * On Init hook
   * @memberof LoaderComponent
   */
  ngOnInit() {}
}
