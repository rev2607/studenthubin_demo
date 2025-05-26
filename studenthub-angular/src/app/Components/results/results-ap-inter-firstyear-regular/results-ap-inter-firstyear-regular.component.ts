import { Component, OnInit } from '@angular/core';
import { StudenthubWebApiService } from '../../../studenthub-web-api.service';
import { BaseResultsComponent } from '../baseresults/baseresults.component';

@Component({
  selector: 'app-results-ap-inter-firstyear-regular',
  templateUrl: './results-ap-inter-firstyear-regular.component.html',
  styleUrls: ['../resultscss/responsive-page.css','../resultscss/result-details.css']
})
export class ResultsApInterFirstYearRegularComponent extends BaseResultsComponent
implements OnInit {

  constructor(private studentWebApiService: StudenthubWebApiService) { 
    super(studentWebApiService);
  }

  ngOnInit(): void {

  }

}
