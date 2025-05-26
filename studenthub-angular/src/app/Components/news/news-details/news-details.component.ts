import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StudenthubWebApiService } from 'src/app/studenthub-web-api.service';
import { NewsAlert } from 'src/app/Model/NewsAlert';

@Component({
  selector: 'app-news-details',
  templateUrl: './news-details.component.html',
  styleUrls: ['./news-details.component.css']
})
export class NewsDetailsComponent implements OnInit {
private UrlKeyWord:string;
public newsDetails: NewsAlert;
  constructor(private route: ActivatedRoute, private studenthubWebApiService: StudenthubWebApiService ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe( paramMap => {
      this.UrlKeyWord =String(paramMap.get('urlkeyword'));    
  });
  // this.studenthubWebApiService.currentNewsAlert.subscribe((x) =>{
  //   if(x && x.NewsId) { 
  //            this.newsDetails = x;
  //   //console.log(x);
  //   }
  // })
  this.studenthubWebApiService.getNewsDetails(this.UrlKeyWord).subscribe((x) => {
    this.newsDetails = x;
  })
  }

}
