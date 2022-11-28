import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import { DataService } from '../services/data.service';
import { Statistic } from '../models/statistic'
import { Event } from '../models/event'
import { Observable, of, ReplaySubject, takeUntil } from "rxjs";
import * as signalR from "@microsoft/signalr";
import { API_BASE_URL } from '../app.module'
import {MatTableDataSource} from "@angular/material/table";
import {MatCheckboxModule} from '@angular/material/checkbox';
import { interval } from 'rxjs';
import { timer } from 'rxjs';
import { map } from 'rxjs';
import { switchAll } from 'rxjs/operators';
import { mergeMap } from 'rxjs/operators';

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
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1)

  constructor(private dataService: DataService, @Inject(API_BASE_URL) baseUrl: string) {
    this.apiBaseUrl = baseUrl;
  }

  getEvents(Id:number) : void {
    this.currentEventId = Id;
    const refreshInterval = timer(0, 5000)
    const events = this.dataService.getEventsByStatisticId(this.currentEventId)
    this.eventDataSource = new MatTableDataSource<Event>;

    refreshInterval
    .pipe(takeUntil(this.destroyed$))
    .pipe(mergeMap(() => events)
      ).subscribe((data: any) => this.eventDataSource.data = data as Event[])

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


    this.connection.on("notifycreateevent", (data:any) =>
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
