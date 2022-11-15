import { Injectable, Inject } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { API_BASE_URL } from './app.module'
import {Observable} from "rxjs";
import {Statistic} from "./models/statistic";
import {Event} from "./models/event"

@Injectable()
export class DataService {
  private apiBaseUrl: string;

  constructor(private http: HttpClient, @Inject(API_BASE_URL) baseUrl: string) {
    this.apiBaseUrl = baseUrl;
  }

  getStatistics() : Observable<Statistic[]>
  {
    return this.http.get<Statistic[]>(this.apiBaseUrl + '/api/Statistic/GetAll');
  }

  getEventsByStatisticId(Id:number) : Observable<Event[]>
  {
    return this.http.get<Event[]>(this.apiBaseUrl + '/api/Statistic/GetEventsByStatisticId?statisticId=${Id}');
  }
}
