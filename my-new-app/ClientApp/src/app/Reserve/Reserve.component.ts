import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router'

@Component({
  selector: 'app-Reserve',
  templateUrl: './Reserve.component.html'
})


export class ReserveComponent {
  public _baseUrl: string;
  public _http: HttpClient;
  public flightId: number;
  public passengersCount: number;
  public flightTitle: string;
  public passengers: passenger[];
  public titles: string[] = [
    "مسافر اول",
    "مسافر دوم",
    "مسافر سوم",
    "مسافر چهارم",
    "مسافر پنجم",
    "مسافر ششم",
    "مسافر هفتم",
    "مسافر هشتم",
    "مسافر نهم",
    "مسافر دهم"
  ];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {
    this._baseUrl = baseUrl;
    this._http = http;
    this.flightId = parseInt(this.route.snapshot.paramMap.get('id'));
    this.passengersCount = parseInt(this.route.snapshot.paramMap.get('count'));

    for (let index = 0; index < this.passengersCount; index++) {
      let p = new passenger();
      p.Title = this.titles[index];

      if (this.passengers == undefined) {
        let a: passenger[] = [p];
        this.passengers = a;
      }
      else {
        this.passengers.push(p);
      }
    }

    // alert(    this.flightId);
    // alert(    this.passengersCount);
    this.flightTitle = "موردی یافت نشد";
    this.GetFlight();
    this.flightTitle + " تعداد مسافران: " + this.passengersCount + " نفر";
  }

  Reserve() {
    let url = this._baseUrl + `Flight/Reserve/${this.flightId}`;
    let token = localStorage.getItem("token");

    if(token==null||token==undefined)
    {
      this.flightTitle = "ابتدا بایستی وارد سیستم شوید";
      window.open(this._baseUrl + "Login", "_self");
      return;
    }

    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'csrf': token
      })
    };

    this._http.post<apiResponse<number>>(url, this.passengers, httpOptions).subscribe(result => {
      this.checkResult(result, (data) => {
        let url = this._baseUrl + "Ticket/" + data;
        console.log("REDIRECT to: " + url);
        window.open(this._baseUrl + "Ticket/" + data, "_self");
      });
    }, error => {
      if (error.status > 400 && error.status < 405)
        window.open(this._baseUrl + "Login", "_self");
      console.error(error); alert("بروز خطا در رزرو بلیط")
    });
  }

  GetFlight() {
    let token = localStorage.getItem("token");
    if(token==null||token==undefined)
    {
      this.flightTitle = "ابتدا بایستی وارد سیستم شوید";
      window.open(this._baseUrl + "Login", "_self");
      return;
    }

    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'csrf': token
      })
    };
    console.log("call  Flight/GetFlight/");
    this._http.get<apiResponse<string>>(this._baseUrl + 'Flight/GetFlight/' + this.flightId ,httpOptions).subscribe(result => {
      this.checkResult(result, (data) => { this.flightTitle = data; console.log(this.flightTitle) });
    }, error => {
      if (error.status > 400 && error.status < 405) {
        this.flightTitle = "ابتدا بایستی وارد سیستم شوید";
        window.open(this._baseUrl + "Login", "_self");
      } else
        this.flightTitle = "ظاهرا اشکالی رخ داده است";
      console.error(error);

    });
  }

  public checkResult<T>(res: apiResponse<T>, next: (value: T) => void) {
    if (res.status == 1) {
      next(res.data);
    } else
      alert(res.message);
  }
}

export class passenger {
  constructor() { }
  Id: number;
  FullName: string;
  NationalityCode: string;
  Age: number;
  Title: string;
}
interface apiResponse<T> {
  data: T;
  status: number;
  totalRow: number;
  message: string;
  exception: string;
}
