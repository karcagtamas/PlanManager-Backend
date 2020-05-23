import { Component, OnInit, Input, ViewChild, OnChanges } from '@angular/core';
import {
  trigger,
  state,
  transition,
  animate,
  style
} from '@angular/animations';
import { EpisodeListDTO } from 'src/app/models';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'ks-episode-table',
  templateUrl: './episode-table.component.html',
  styleUrls: ['./episode-table.component.scss'],
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
export class EpisodeTableComponent implements OnInit, OnChanges {
  @Input() source: EpisodeListDTO[];
  dataSource: MatTableDataSource<EpisodeListDTO>;
  displayedColumns: string[] = ['number'];

  expanded: EpisodeListDTO | null;

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
    this.dataSource = new MatTableDataSource<EpisodeListDTO>(this.source);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }
}
