import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ReserveComponent } from '../Reserve/Reserve.component';

@Component({
  selector: 'app-Ticket',
  templateUrl: './Ticket.component.html'
})


export class TicketComponent {

  public _baseUrl: string;
  public _http: HttpClient;
  public reserveId: number;
  public data: ReseveTickets;
  public err: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this._baseUrl = baseUrl;
    this._http = http;
    this.reserveId = parseInt(this.route.snapshot.paramMap.get('id'));
    this.GetTickets();
  }

  GetTickets() {
    let token = localStorage.getItem("token");
    if(token==null||token==undefined)
    {
      window.open(this._baseUrl + "Login", "_self");
      return;
    }

    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'csrf': token
      })
    };
    this._http.get<apiResponse<ReseveTickets>>(this._baseUrl + 'Flight/GetTickets/' + this.reserveId , httpOptions).subscribe(result => {
      this.checkResult(result, (data) => {
        this.data = data;
        console.log(this.data)
      });

    }, error => {
      console.error(error)
      if (error.status > 400 && error.status < 405)
        window.open(this._baseUrl + "Login", "_self");
    });
  }

  public checkResult<T>(res: apiResponse<T>, next: (value: T) => void) {
    if (res.status == 1) {
      next(res.data);
    } else
      alert(res.message);
  }
}
interface ReseveTickets {
  ReserveId: number;
  userName:string
  ReseveDate: string;
  Tickets: Ticket[];
}
interface Ticket {
  publicFullName: string;
  Price: number;
  Source: string;
  Destination: string;
  flightDate: string;
  TicketCode: string;
}

interface apiResponse<T> {
  data: T;
  status: number;
  totalRow: number;
  message: string;
  exception: string;
}
