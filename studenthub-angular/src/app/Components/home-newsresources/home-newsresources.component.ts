import { Component, OnInit } from '@angular/core';
import { StudenthubWebApiService } from 'src/app/studenthub-web-api.service';
import { Router } from '@angular/router';
import { NewsAlert } from 'src/app/Model/NewsAlert';

@Component({
  selector: 'app-home-newsresources',
  templateUrl: './home-newsresources.component.html',
  styleUrls: ['./home-newsresources.component.css']
})
export class HomeNewsresourcesComponent implements OnInit {

  constructor(private studenthubWebApiService: StudenthubWebApiService) { }
  latestNews: NewsAlert[] = [];
    ngOnInit(): void {
  this.studenthubWebApiService.getLatestNews(7).subscribe((x) =>{
    this.latestNews = x;
  });
    }
  }
