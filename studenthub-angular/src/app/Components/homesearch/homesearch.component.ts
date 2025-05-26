import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-homesearch',
  templateUrl: './homesearch.component.html',
  styleUrls: ['./homesearch.component.css']
})
export class HomesearchComponent implements OnInit {

  searchtext = "";

  constructor(private router: Router) {

   
  }
  results(parameter:string) {
    this.router.navigateByUrl("/"+parameter);
  }
  ngOnInit(): void {
  }

  search_Click() {

    if (this.searchtext != (null || "")) {
      if (this.searchtext.toLowerCase().indexOf("result") > -1) {
        this.results("results");
      }
      if (this.searchtext.toLowerCase().indexOf("college") > -1) {
        this.results("colleges");
      }
      if (this.searchtext.toLowerCase().indexOf("notification") > -1) {
        this.results("examnotification");
      }
      if (this.searchtext.toLowerCase().indexOf("schedule") > -1) {
        this.results("examschedule");
      }
    }
  }
}
