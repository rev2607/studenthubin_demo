import { Component, OnInit } from '@angular/core';
import { StudenthubWebApiService } from '../../../studenthub-web-api.service';
import { BaseResultsComponent } from '../baseresults/baseresults.component';

@Component({
  selector: 'app-results-ts-inter-firstyear-vocational',
  templateUrl: './results-ts-inter-firstyear-vocational.component.html',
  styleUrls: ['../resultscss/responsive-page.css','../resultscss/result-details.css']
})
export class ResultsTsInterFirstYearVocationalComponent extends BaseResultsComponent
implements OnInit {

  constructor(private studentWebApiService: StudenthubWebApiService) { 
    super(studentWebApiService);
  }

  ngOnInit(): void {

  }

}
