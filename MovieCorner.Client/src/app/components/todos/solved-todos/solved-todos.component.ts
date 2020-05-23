import { Component, OnInit } from '@angular/core';

/**
 * Solved ToDos Component
 * Display user's all solved todos
 * @export
 * @class SolvedTodosComponent
 * @implements {OnInit}
 */
@Component({
  selector: 'ks-solved-todos',
  templateUrl: './solved-todos.component.html',
  styleUrls: ['./solved-todos.component.scss']
})
export class SolvedTodosComponent implements OnInit {
  /**
   * Creates an instance of SolvedTodosComponent.
   * @memberof SolvedTodosComponent
   */
  constructor() {}

  /**
   * On Init hook
   *
   * @memberof SolvedTodosComponent
   */
  ngOnInit() {}
}
