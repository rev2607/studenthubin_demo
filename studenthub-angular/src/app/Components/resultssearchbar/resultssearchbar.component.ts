import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StudenthubWebApiService } from '../../studenthub-web-api.service';

@Component({
  selector: 'app-resultssearchbar',
  templateUrl: './resultssearchbar.component.html',
  styleUrls: ['./resultssearchbar.component.css']
})
export class ResultssearchbarComponent implements OnInit {

  filterData : any;

  statescollection: any;
  constructor(private router: ActivatedRoute, private studentHubWebApi: StudenthubWebApiService) {

    //Set Controls Data by calling API
    this.studentHubWebApi.getResultSearchFilters().then((response) => {

      this.filterData = response;
     // this.statollection = Array<ExamsDetail>[];

      for (var i = 0; i < this.filterData.States.length; i++) {
        this.statescollection[i] = {
          name: this.filterData.States[i],
          value: this.filterData.States[i]
        }
      }
    }).catch((error) => {
      console.log("Server error : Could not get the details of this record.", "");
    });;
  }
  resultTabActive = "";
  examNotificationActive = "";
  examScheduleActive = "";

  ngOnInit(): void {
    
    this.router.queryParams
      .subscribe(params => {
       // console.log(params);
        if (params.hasOwnProperty("pagetype")) {
          if (params["pagetype"] == "results") {
            this.resultTabActive = "active";
            this.examNotificationActive = "";
            this.examScheduleActive = "";
          }
          if (params["pagetype"] == "examnotification") {
            this.resultTabActive = "";
            this.examNotificationActive = "active";
            this.examScheduleActive = "";
          }
          if (params["pagetype"] == "examschedule") {
            this.resultTabActive = "";
            this.examNotificationActive = "";
            this.examScheduleActive = "active";
          }
        }
      });
  }
}
