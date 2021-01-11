import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(@Inject('BASE_URL') baseUrl: string)
  {
    window.open(baseUrl + "SearchFlights", "_self");
  }
}
