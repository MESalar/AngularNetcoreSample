import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-Login',
  templateUrl: './Login.component.html'
})


export class LoginComponent {
  public _baseUrl: string;
  public _http: HttpClient;
  public loginInfo: LoginModel;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
    this._http = http;
    this.loginInfo = new LoginModel();
    console.log("!!!!!!!!!!!!!!!!!!!!!REFERER");
    console.log(document.referrer);
  }


  Login() {
    let url = this._baseUrl + `user/Login`;

    console.log("Login");
    console.log(this.loginInfo);
    this._http.post<apiResponse<loginResponse>>(url, this.loginInfo).subscribe(result => {
      this.checkResult(result, (data) => {
        if (data == null || data == undefined) {
          alert("نام کاربری یا رمز عبوور اشتباه است");
          return;
        }
        localStorage.setItem("token", data.token);
        localStorage.setItem("userName", data.displayName);
        console.log("LOGIN RESPONSE: " + data);
        console.log(localStorage.getItem("token"));
        if (document.referrer != null && document.referrer != undefined && document.referrer != "" && document.referrer.indexOf("Login") <= 0)
          window.open(document.referrer, "_self");
        else
          window.open(this._baseUrl + "SearchFlights", "_self");
      });
    }, error => { console.error(error); alert("نام کاربری یا رمز عبور اشتباه است"); });
  }


  public checkResult<T>(res: apiResponse<T>, next: (value: T) => void) {
    if (res.status == 1) {
      next(res.data);
    } else
      alert(res.message);
  }
}


export class LoginModel {
  constructor() { }
  NationalityCode: string;
  Password: string;
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
