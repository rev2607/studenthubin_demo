import { Component, OnInit } from '@angular/core';
import { StudenthubWebApiService } from '../../../studenthub-web-api.service';
import { BaseResultsComponent } from '../baseresults/baseresults.component';

@Component({
  selector: 'app-results-ts-eamcet-agriculture-medical',
  templateUrl: './results-ts-eamcet-agriculture-medical.component.html',
  styleUrls: ['../resultscss/responsive-page.css','../resultscss/result-details.css']
})
export class ResultsTsEamcetAgricultureMedicalComponent extends BaseResultsComponent
implements OnInit {

  constructor(private studentWebApiService: StudenthubWebApiService) { 
    super(studentWebApiService);
  }

  ngOnInit(): void {

  }

}
