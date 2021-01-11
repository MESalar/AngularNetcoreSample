import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { SearchFlightsComponent } from './searchFlights/SearchFlights.component';
import { RegisterComponent } from './Register/Register.component';
import { LoginComponent } from './Login/Login.component';
import { ReserveComponent } from './Reserve/Reserve.component';
import { TicketComponent } from './Ticket/Ticket.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    SearchFlightsComponent,
    ReserveComponent,
    TicketComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'SearchFlights', component: SearchFlightsComponent },
      { path: 'Reserve/:id/:count', component: ReserveComponent },
      { path: 'Ticket/:id', component: TicketComponent },
      { path: 'Login', component: LoginComponent },
      { path: 'Register', component: RegisterComponent } ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
