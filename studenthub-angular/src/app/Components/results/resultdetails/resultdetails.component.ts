import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { StudenthubWebApiService } from '../../../studenthub-web-api.service';
import { Location } from '@angular/common';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-resultdetails',
  templateUrl: './resultdetails.component.html',
  styleUrls: [
    './resultdetails.component.css',
    '../resultscss/responsive-page.css',
    '../resultscss/result-details.css',
  ],
})
export class ResultdetailsComponent implements OnInit {
  public examId: string = '';
  public examName: string = '';
  public hallTicketNumber: string = '';
  public name: string = '';
  public year: string = '';
  private toSendhallTicketNumber: string = '';
  private toSendName: string = '';
  public isSeachDone: boolean = false;
  public result: any;
  public error: any;
  @ViewChild('print_element')
  public print_element: any;
  constructor(
    private router: ActivatedRoute,
    private location: Location,
    private studentHubWebApi: StudenthubWebApiService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.router.queryParams.subscribe((params: Params) => {
      this.examId = params['examId'];
     // console.log(this.examId);
      this.examName = params['examName'];
      this.year = params['year'];
    });

    // this.router.params.subscribe((x) => {
    //   if (x['url']) {
    //     var splitValues = x['url'].split('-');
    //     this.examId = splitValues[0];
    //     for (var counter = 1; counter < splitValues.length - 1; counter++) {
    //       if (this.examName) {
    //         this.examName = this.examName + ' ' + splitValues[counter];
    //       } else {
    //         this.examName = splitValues[counter];
    //       }
    //     }
    //    // console.log(this.examName);
    //     this.year = splitValues[splitValues.length - 1];
    //     // this.location.replaceState("resultdetails/"+ this.examId+"-" + this.examName.replace(/ /g,"-") + "-" + this.year);
    //   }
    // });
    this.studentHubWebApi.currenthallTicketNumber.subscribe((x: any) => {
      if (x && x.examId) {
        this.examId = x.examId;
        this.examName = x.examName;
        this.year = x.year;
        // this.location.replaceState(
        //   'resultdetails/' +
        //     this.examId +
        //     '-' +
        //     this.examName?.replace(/ /g, '-') +
        //     '-' +
        //     this.year
        // );
      }
    });

    // this.router.url
    //this.location.replaceState("Results/"  + this.examName.replace(/ /g,"-") + "-" + this.year);
  }

  public searchResults(): void {
this.isSeachDone = false;
this.error = "";
    this.spinner.show();

    this.studentHubWebApi
      .getResultBasedOnHallTicketOrName(
        this.toSendhallTicketNumber,
        this.toSendName,
        this.examId,
        this.year
      )
      .subscribe((x: any) => {
        //console.log(x);

        if (x.IsResultSet) {
          this.result = x;
          this.isSeachDone = true;
          this.studentHubWebApi.sendResult(this.result);
          this.studentHubWebApi.sendHallTicketNumber({
            hallTicketNumber: this.hallTicketNumber,
            examName: this.examName,
            year: this.year,
            examId: this.examId,
          });
        } else {
          this.error = x;
        }
        this.spinner.hide();
      },
        (err) =>{
          this.spinner.hide();
          console.log(err);
        }
      );
    
  }

  public searchByName(): void {
    this.toSendName = this.name;
    this.toSendhallTicketNumber = '';
    this.searchResults();
  }

  public searchByHallTicket(): void {
    this.toSendName = '';
    this.toSendhallTicketNumber = this.hallTicketNumber;
    this.searchResults();
  }
}
