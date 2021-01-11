import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
@Component({
  selector: 'app-Register',
  templateUrl: './Register.component.html'
})


export class RegisterComponent {
  public registerInfo: Register;
  public _baseUrl: string;
  public _http: HttpClient;
  public submitted: boolean;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.submitted = false;
    this._baseUrl = baseUrl;
    this._http = http;
    this.registerInfo = new Register();
  }

  public Register() {
    let url = this._baseUrl + `User/Register`;
    this._http.post<apiResponse<loginResponse>>(url, this.registerInfo,).subscribe(result => {
      this.checkResult(result, (data) => {
        localStorage.setItem("token", data.token);
        localStorage.setItem("userName", data.displayName);
        console.log(localStorage.getItem("token"));
        if (document.referrer != null && document.referrer != undefined && document.referrer !="")
          window.open(document.referrer, "_self");
        else
          window.open(this._baseUrl + "SearchFlights", "_self");
      });
    }, error => console.error(error));
  }

  public checkResult<T>(res: apiResponse<T>, next: (value: T) => void) {
    if (res.status == 1) {
      next(res.data);
    } else
      alert(res.message);
  }

}

export class Register {
  constructor() { }
  NationalityCode: string;
  Name: string;
  lastName: string;
  Password: string;
  PasswordConfirm: string;
}
interface loginResponse {
  token: string;
  displayName: string;
}
interface apiResponse<T> {
  data: T;
  status: number;
  totalRow: number;
  message: string;
  exception: string;
}
