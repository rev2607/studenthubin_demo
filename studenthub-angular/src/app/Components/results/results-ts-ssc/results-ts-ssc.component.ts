import { Component, OnInit } from '@angular/core';
import { StudenthubWebApiService } from '../../../studenthub-web-api.service';
import { BaseResultsComponent } from "../baseresults/baseresults.component"

@Component({
  selector: 'app-results-ts-ssc',
  templateUrl: './results-ts-ssc.component.html',
  styleUrls: ['../resultscss/responsive-page.css','../resultscss/result-details.css']
})
export class ResultsTsSSCComponent extends BaseResultsComponent
implements OnInit {

  constructor(private studentWebApiService: StudenthubWebApiService) { 
    super(studentWebApiService);
  }

  ngOnInit(): void {

  }

}
