import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import { DataService } from '../services/data.service';
import { Statistic } from '../models/statistic'
import { Event } from '../models/event'
import { BehaviorSubject, Observable, of, ReplaySubject, Subscription, takeUntil } from "rxjs";
import * as signalR from "@microsoft/signalr";
import { API_BASE_URL } from '../app.module'
import {MatTableDataSource} from "@angular/material/table";
import { interval } from 'rxjs';
import { startWith, switchMap } from 'rxjs/operators';
@Component({
  selector: 'app-statistics-list',
  styleUrls: ["statistics-list.component.css"],
  templateUrl: './statistics-list.component.html'
})

export class StatisticsListComponent implements OnInit, OnDestroy {
  currentEventId:number;
  statisticDataSource: MatTableDataSource<Statistic>;
  eventDataSource: MatTableDataSource<Event>;
  private apiBaseUrl: string;
  connection: signalR.HubConnection;
  displayedColumns: string[] = ["userName", "updateDate", "clientVersion", "os"];
  displayedColumnsForEvents: string[] = ["name", "date"];
  private checkboxSubject$ = new BehaviorSubject(false);

get checkboxSubject(): boolean {
  return this.checkboxSubject$.value;
}
set checkboxSubject(v: boolean) {
  this.checkboxSubject$.next(v);
}

  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1)

  refreshSubscription: Subscription;

  constructor(private dataService: DataService, @Inject(API_BASE_URL) baseUrl: string) {
    this.apiBaseUrl = baseUrl;
  }

  getEvents(Id:number) : void 
  {
    this.currentEventId = Id;
    this.eventDataSource = new MatTableDataSource<Event>;
    this.dataService.getEventsByStatisticId(this.currentEventId).pipe(takeUntil(this.destroyed$)).subscribe((data: any) => this.eventDataSource.data = data as Event[])
  }

  autoRefreshCheckbox()
  {
    console.log(this.checkboxSubject);

    if (this.checkboxSubject)
    this.refreshSubscription = interval(3000)
    .pipe(
      startWith(1),
      switchMap(() => this.dataService.getEventsByStatisticId(this.currentEventId)),
      takeUntil(this.destroyed$))
    .subscribe((data: any) => this.eventDataSource.data = data as Event[])
    else {
      this.refreshSubscription.unsubscribe();}
  }

  getStatistics()
  {
    this.statisticDataSource = new MatTableDataSource<Statistic>;
    this.dataService.getStatistics().pipe(takeUntil(this.destroyed$)).subscribe((data: any) => this.statisticDataSource.data = data as Statistic[])
  }

  createConnection() : void
  {
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl(this.apiBaseUrl + '/statisticListComponent')
    .build();

    this.connection.start()
    .then(() => console.log('Connection started'))
    .catch(err => console.log('Error while starting connection: ' + err))


    this.connection.on("notifycreateeven", (data:any) =>
    {
      this.getEvents(this.currentEventId);
    });

    this.connection.on("notifycreatestatistic", (data: Statistic) =>
    {
      this.statisticDataSource.data.push(data);
      this.statisticDataSource.data = [...this.statisticDataSource.data];
    });
  }

  ngOnInit() {
    this.createConnection();
    this.getStatistics();
  }

  ngOnDestroy() {
    this.connection?.stop();
    this.destroyed$.next(true);
    this.destroyed$.unsubscribe();
    this.destroyed$.complete();
  }
}
