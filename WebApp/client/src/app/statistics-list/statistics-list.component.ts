import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import { DataService } from '../services/data.service';
import { Statistic } from '../models/statistic'
import { Event } from '../models/event'
import { ReplaySubject, takeUntil } from "rxjs";
import * as signalR from "@microsoft/signalr";
import { API_BASE_URL } from '../app.module'

@Component({
  selector: 'app-statistics-list',
  styleUrls: ["statistics-list.component.css"],
  templateUrl: './statistics-list.component.html'
})

export class StatisticsListComponent implements OnInit, OnDestroy {
  private apiBaseUrl: string;
  connection: signalR.HubConnection;
  statistics: Statistic[];
  events: Event[];
  displayedColumns: string[] = ["userName", "updateDate", "clientVersion", "os"];
  displayedColumnsForEvents: string[] = ["name", "date"];
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1)

  constructor(private dataService: DataService, @Inject(API_BASE_URL) baseUrl: string) {
    this.apiBaseUrl = baseUrl;
  }

  getEvents(Id:number) {
    this.dataService.getEventsByStatisticId(Id).pipe(takeUntil(this.destroyed$)).subscribe((data: any) => this.events = data as Event[])
  }

  getStatistics()
  {
    this.dataService.getStatistics().pipe(takeUntil(this.destroyed$)).subscribe((data: any) => this.statistics = data as Statistic[])
  }

  createConnection() : void
  {
    this.connection = new signalR.HubConnectionBuilder()
    .withUrl(this.apiBaseUrl + '/statistic')
    .build();

    this.connection.start()
    .then(() => console.log('Connection started'))
    .catch(err => console.log('Error while starting connection: ' + err))

    this.connection.on("notifycreatestatistic", () => this.getStatistics);
  }

  ngOnInit() {
    this.createConnection();
    this.getStatistics();
  }

  ngOnDestroy() {
    this.connection?.stop();
    this.destroyed$.next(true);
    this.destroyed$.complete();
  }
}
