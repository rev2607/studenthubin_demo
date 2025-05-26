import { Component, OnInit } from '@angular/core';
import { NewsAlert } from 'src/app/Model/NewsAlert';
import { StudenthubWebApiService } from 'src/app/studenthub-web-api.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css'],
})
export class NewsComponent implements OnInit {
   p: number = 1;
newsAlerts: Array<NewsAlert> = [];
  constructor(private studentHubWebApi: StudenthubWebApiService, private spinner: NgxSpinnerService) {}
  ngOnInit(): void {
this.spinner.show();
this.studentHubWebApi.getNewsAlerts().subscribe((x: any) => {
  this.newsAlerts = x;
  this.spinner.hide();
},
(err) =>{
  this.spinner.hide();
  console.log(err);
})
  }

  // public onClick(id: number): void {
  //   let currentNews: NewsAlert= new NewsAlert();
  //   if(this.newsAlerts.find((x => x.NewsId === id))) {
  //     currentNews = this.newsAlerts.filter(x => x.NewsId === id)[0];
  //   }
  //   this.studentHubWebApi.sendNewsAlertInfo(currentNews);
  // }

}
