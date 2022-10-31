import {Component, OnDestroy, OnInit} from '@angular/core';
import { DataService } from '../data.service';
import { Statistic } from '../models/statistic'
import {ReplaySubject, takeUntil} from "rxjs";

@Component({
  selector: 'app-statistics-list',
  templateUrl: './statistics-list.component.html'
})


export class StatisticsListComponent implements OnInit, OnDestroy {
  statistics: Statistic[];
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1)

  constructor(private dataService: DataService, ) { }

  ngOnInit() {
    this.dataService.getStatistics().pipe(takeUntil(this.destroyed$)).subscribe((data: any) => this.statistics = data as Statistic[])
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
