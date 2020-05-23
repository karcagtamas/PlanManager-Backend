import { Component, OnInit, ViewChild, Input, OnChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SeasonListDTO } from 'src/app/models';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import {
  animate,
  state,
  style,
  transition,
  trigger
} from '@angular/animations';

@Component({
  selector: 'ks-season-table',
  templateUrl: './season-table.component.html',
  styleUrls: ['./season-table.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      )
    ])
  ]
})
export class SeasonTableComponent implements OnInit, OnChanges {
  @Input() source: SeasonListDTO[];
  dataSource: MatTableDataSource<SeasonListDTO>;
  displayedColumns: string[] = ['number', 'countOfEpisodes'];

  expanded: SeasonListDTO | null;

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor() {}

  ngOnInit(): void {
    this.setTable();
  }

  ngOnChanges(): void {
    this.setTable();
  }

  public setTable(): void {
    this.dataSource = new MatTableDataSource<SeasonListDTO>(this.source);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
}
