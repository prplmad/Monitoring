import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { StatisticsListComponent } from './statistics-list/statistics-list.component';
import { InjectionToken } from '@angular/core';
import {DataService} from "./data.service";
import { environment } from "../environments/environment";

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@NgModule({
  declarations: [
    AppComponent,
    TopBarComponent,
    StatisticsListComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: StatisticsListComponent },
    ])
  ],
  providers: [{ provide: API_BASE_URL, useValue: environment.apiRoot}, DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
