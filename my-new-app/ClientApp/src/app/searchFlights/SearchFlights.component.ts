import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-SearchFlights',
  templateUrl: './SearchFlights.component.html'
})


export class SearchFlightsComponent {
  public Flights: Flight[];
  public Airports: SelectItem[];
  public _baseUrl: string;
  public _http: HttpClient;
  public SourceId: number;
  public DestinationId: number;
  public flightDate: string;
  public quantity: number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
    this.GetAirports();
    this.SourceId = 2;
    this.DestinationId=3;
    this.quantity =1;
    this.flightDate="1399/10/22";
this.searchFlights();
  }

  searchFlights() {
    let url = this._baseUrl + `Flight/Search/${this.SourceId}/${this.DestinationId}/${this.quantity}/${this.flightDate}`;
    this._http.get<apiResponse<Flight[]>>( url).subscribe(result => {
      this.checkResult(result, (data) => { this.Flights = data; console.log(this.Flights); });
    }, error => console.error(error));
  }
  GetAirports() {
    this._http.get<apiResponse<SelectItem[]>>(this._baseUrl + 'Flight/GetAirports').subscribe(result => {
      this.checkResult(result,(data)=>{this.Airports = data; console.log(this.Airports)});
    }, error => console.error(error));
  }

  public checkResult<T>(res: apiResponse<T>, next: (value: T) => void) {
    if (res.status == 1)
    {
      next(res.data);
    }else
      alert(res.message);
  }
  public alertflightDate()
  {
    alert(this.flightDate);
  }

  public alertQ()
  {
    alert(this.quantity);
  }
  // public Post<T>(arg:any):T {

  // }
}

interface Flight {
  id: number;
  title: string;
  source: string;
  destination: string;
  date: string;
  price: string;
  quantity: number;
}

interface SelectItem {
  Id: number;
  Title: string;
}

interface date {
  day: number;
  month: number;
  year: number;
}

interface apiResponse<T> {
  data: T;
  status: number;
  totalRow: number;
  message: string;
  exception: string;
}
