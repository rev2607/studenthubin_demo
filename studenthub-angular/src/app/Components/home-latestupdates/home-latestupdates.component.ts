import { StudenthubWebApiService } from './../../studenthub-web-api.service';
import {  Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ResultDetails } from 'src/app/Model/ResultDetails';
import { NewsAlert } from 'src/app/Model/NewsAlert';

@Component({
  selector: 'app-home-latestupdates',
  templateUrl: './home-latestupdates.component.html',
  styleUrls: ['./home-latestupdates.component.css']
})
export class HomeLatestupdatesComponent implements OnInit {

  constructor(private router: Router, private studenthubWebApiService: StudenthubWebApiService) { }
 latestResults: Array<ResultDetails> = [];
upcomingResults: Array<ResultDetails> = [];
examNotifications: Array<NewsAlert> = [];

  ngOnInit(): void {

    this.studenthubWebApiService.getExamNotications().subscribe((x) => {
       this.examNotifications = x;
    });
    // this.latestResults.push({Name: 'TS Inter 1st Year Results 2022 Regular', Url: 'resultdetails?examId=TSINTR1REG&examName=Telangana%20Regular%20Intermediate%20First%20Year%20%2F%2011th&year=2022'});
    // this.latestResults.push({Name: 'TS Inter 2nd Year Results 2022 Regular', Url: 'resultdetails?examId=TSINTR2REG&examName=Telangana%20Regular%20Intermediate%20Second%20Year%20%2F%2012th&year=2022'});
    // this.latestResults.push({Name: 'TS Inter 1st Year Results 2022 Vocational', Url: 'resultdetails?examId=TSINTR1Voc&examName=Telangana%20Vocational%20Intermediate%20First%20Year%20%2F%2011th&year=2022'});
    // this.latestResults.push({Name: 'TS Inter 2nd Year Results 2022 Vocational', Url: 'resultdetails?examId=TSINTR2Voc&examName=Telangana%20Vocational%20Intermediate%20Second%20Year%20%2F%2012th&year=2022'});
  this.latestResults.push({Name: 'AP Eapcet Engineering Results 2022', Url:'resultdetails?examId=APEAPENG&examName=Andhra%20Pradesh%20EAPCET%20Engineering&year=2022'});
  this.latestResults.push({Name: 'AP Eapcet Agriculture/Medical Results 2022', Url:'resultdetails?examId=APEAPAM&examName=Andhra%20Pradesh%20EAPCET%20Agriculture%2FMedical&year=2022'});
  this.upcomingResults.push({Name: 'TS Eamcet Results 2022', Url:''});
  }

  onResultClick(url: string): void {
    if(url) {
    this.router.navigateByUrl(url);
    }
  }

  onNotificationClick(UrlKeyWord: string): void {
   this.router.navigate(["exam-notifications", UrlKeyWord]);
  }

}
