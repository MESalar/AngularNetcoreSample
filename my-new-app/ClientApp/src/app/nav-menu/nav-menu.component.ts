import { stringify } from '@angular/compiler/src/util';
import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
public userName:string;
constructor(){
  this.userName = localStorage.getItem("userName") != null && localStorage.getItem("userName") != undefined ? localStorage.getItem("userName"):"مهمان" ;
  console.log("!!!!!userName: " +this.userName);
  console.log("!!!!!LOCALS: " +localStorage.getItem("userName"));
}
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
