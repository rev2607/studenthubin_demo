import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { StudenthubWebApiService } from '../../../studenthub-web-api.service';

@Component({
  selector: 'app-baseresults',
  templateUrl: './baseresults.component.html',
  styleUrls: ['./baseresults.component.css'],
})
export class BaseResultsComponent implements OnInit {
  public result: any;
  public hallTicketNumber: string;
  public name: string;
  public examId: string;
  public year: string;
  constructor(private studentHubWebApi: StudenthubWebApiService) {
    this.studentHubWebApi.currentResult.subscribe((x: any) => {
      this.result = x;
    });
    this.studentHubWebApi.currenthallTicketNumber.subscribe((x: any) => {
      this.hallTicketNumber = x.hallTicketNumber;
      this.name = x.name;
      this.examId = x.examId;
      this.year = x.year;
    });
  }

  ngOnInit(): void {
    this.studentHubWebApi.currentResult.subscribe((x: any) => {
      this.result = x;
    });
  }
}
