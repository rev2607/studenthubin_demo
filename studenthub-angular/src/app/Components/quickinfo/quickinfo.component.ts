import { StudenthubWebApiService } from 'src/app/studenthub-web-api.service';
import { NewsAlert } from './../../Model/NewsAlert';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-quickinfo',
  templateUrl: './quickinfo.component.html',
  styleUrls: ['./quickinfo.component.css']
})
export class QuickinfoComponent implements OnInit {

  constructor(private studenthubWebApiService: StudenthubWebApiService) { }
latestNews: NewsAlert[] = [];
  ngOnInit(): void {
this.studenthubWebApiService.getLatestNews(5).subscribe((x) =>{
  this.latestNews = x;
});
  }

}
