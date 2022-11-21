import {Component, OnDestroy, OnInit} from '@angular/core';
import { DataService } from '../data.service';
import { Statistic } from '../models/statistic'
import { Event } from '../models/event'
import { ReplaySubject, takeUntil } from "rxjs";

@Component({
  selector: 'app-statistics-list',
  styleUrls: ["statistics-list.component.css"],
  templateUrl: './statistics-list.component.html'
})


export class StatisticsListComponent implements OnInit, OnDestroy {
  statistics: Statistic[];
  events: Event[];
  displayedColumns: string[] = ["id", "userName", "updateDate", "clientVersion", "os"];
  displayedColumnsForEvents: string[] = ["name", "date"];
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1)

  constructor(private dataService: DataService, ) { 
    
  }

  getRecord(Id:number) {
    this.dataService.getEventsByStatisticId(Id).pipe(takeUntil(this.destroyed$)).subscribe((data: any) => this.events = data as Event[])
  }

  ngOnInit() {
    this.dataService.getStatistics().pipe(takeUntil(this.destroyed$)).subscribe((data: any) => this.statistics = data as Statistic[])
  }

  ngOnDestroy() {
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
