import { Component, OnInit } from '@angular/core';
import { StudenthubWebApiService } from '../../../studenthub-web-api.service';
import { BaseResultsComponent } from '../baseresults/baseresults.component';

@Component({
  selector: 'app-results-ts-eamcet-engineering',
  templateUrl: './results-ts-eamcet-engineering.component.html',
  styleUrls: ['../resultscss/responsive-page.css','../resultscss/result-details.css']
})
export class ResultsTsEamcetEngineeringComponent extends BaseResultsComponent
implements OnInit {

  constructor(private studentWebApiService: StudenthubWebApiService) { 
    super(studentWebApiService);
  }

  ngOnInit(): void {

  }

}
