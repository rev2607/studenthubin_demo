import { Component, forwardRef, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Exam, ExamsDetail, SearchResultFilters } from "../../Model/SearchResultFilters";
import { StudenthubWebApiService } from '../../studenthub-web-api.service';

@Component({
  selector: 'app-searchresults-main',
  templateUrl:'./searchresults-main.component.html',
  styleUrls: ['./searchresults-main.component.css'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SearchresultsMainComponent),
      multi: true
    }
  ]
})
export class SearchresultsMainComponent implements OnInit {

  public filterData: SearchResultFilters;
  public exams: string[] = [];
  public selectedState: string = "";
  private selectedExamTypeValue: string = "";
 // public selectedExam: string ="";
  public selectedYear: string ="";

  private SELECT_STATE: string = "Select State";
  private SELECT_EXAM_TYPE: string = "Select Examination";
  //private SELECT_EXAM: string = "Select Exam";
  private SELECT_YEAR: string = "Select Year";

  constructor(private router: ActivatedRoute, private studentHubWebApi: StudenthubWebApiService) {
     this.selectedState = this.SELECT_STATE;
     this.selectedExamType = this.SELECT_EXAM_TYPE;
    // this.selectedExam = this.SELECT_EXAM;
     this.selectedYear = this.SELECT_YEAR;
  }

  ngOnInit(): void {
    this.studentHubWebApi.getResultSearchFilters().then((response) => {
      this.filterData = response;
      this.exams = this.filterData.ExamsDetails.map((x: ExamsDetail) => x.ExamName);
    }).catch((error) => {
      console.log("Server error : Could not get the details of this record.", "");
    });;
  }

  public set selectedExamType(selectedExamType: string) {

    this.selectedExamTypeValue = selectedExamType;
    if (selectedExamType != this.SELECT_EXAM_TYPE) {
        this.exams = this.filterData.ExamsDetails.filter((x: ExamsDetail) => x.ExamType == selectedExamType && x.State == this.selectedState).map((x) => x.ExamName);
    }
  }

  public get selectedExamType(): string {
    return this.selectedExamTypeValue;
  }

}
