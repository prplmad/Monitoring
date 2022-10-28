import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Statistic } from './statistic';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [DataService]
})

export class AppComponent implements OnInit {
  statistics: Statistic[];

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.loadStatistics();
  }

  loadStatistics() {
    this.dataService.getStatistics()
      .subscribe((data: any) => this.statistics = data as Statistic[]);
  }
}

