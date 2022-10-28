import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Injectable()
export class DataService {

  private url = "/api/Statistic";

  constructor(private http: HttpClient) {
  }

  getStatistics() {
    return this.http.get(this.url + '/GetAllStatistics');
  }
}
