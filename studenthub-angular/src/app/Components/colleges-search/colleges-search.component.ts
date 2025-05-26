import {
  CollegeResults,
  CollegesResultsFilters,
  College,
  StateCities,
  InputBind,
} from './../../Model/CollegeResultFilters';
import { Component, OnInit } from '@angular/core';
import { StudenthubWebApiService } from '../../studenthub-web-api.service';
import { forkJoin } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-colleges-search',
  templateUrl: './colleges-search.component.html',
  styleUrls: ['./colleges-search.component.css'],
})
export class CollegesSearchComponent implements OnInit {
  p: number = 1;

  constructor(private studenthubWebApiService: StudenthubWebApiService, private spinner: NgxSpinnerService) { }
  public colleges: Array<College> = [];
  public cities: Array<InputBind> = [];
  public types: Array<InputBind> = [];
  public subTypes: Array<InputBind> = [];
  public collegesCount: number;
  public collegeResultFilters: CollegesResultsFilters;
  public filteredColleges: Array<College> = [];

  ngOnInit(): void {
    this.spinner.show();
    forkJoin(
      this.studenthubWebApiService.getCollegesResults(),
      this.studenthubWebApiService.getCollegeFilters()
    ).subscribe((x: [CollegeResults, CollegesResultsFilters]) => {
      this.spinner.show();
      this.colleges = x[0].colleges;
      this.filteredColleges = this.colleges;
      this.collegesCount = x[0].collegesCount;
      this.collegeResultFilters = x[1];
      Object.values(this.collegeResultFilters.StatesAndCities).map((z) =>
        z.Cities.forEach((y: string) => this.cities.push({ Name: y, isChecked: false, isVisible: true })));
      this.collegeResultFilters.CollegeTypes.forEach((n) => { this.types.push({ Name: n, isChecked: false, isVisible: true }) });
      this.collegeResultFilters.CollegeSubTypes.forEach((n) => { this.subTypes.push({ Name: n, isChecked: false, isVisible: true }) })
      this.spinner.hide();
    },
      (err) => {
        this.spinner.hide();
        console.log(err);
      });
  }

  filterState($event: any) {
   const filterString: String = $event.target.value;
   this.collegeResultFilters.StatesAndCities.forEach((x) =>{
if(!(x.State.toUpperCase().indexOf(filterString.toUpperCase()) >-1)) {
  x.isHidden = true;  
}
else{
  x.isHidden = false;
}
   });
  }

  filterCity($event: any){
    const filterString: String = $event.target.value;
    this.cities.forEach((x) =>{
      if(!(x.Name.toUpperCase().indexOf(filterString.toUpperCase()) >-1)) {
        x.isVisible = false;  
      }
      else{
        x.isVisible = true;
      }
         });
  }

  onStateChange() {
  
    let selectedStates: string[] = this.collegeResultFilters.StatesAndCities.filter((x) => x.isChecked).map((x) => x.State);
    if (selectedStates.length) {
      this.cities = [];
      Object.values(this.collegeResultFilters.StatesAndCities.filter((x) => x.isChecked)).map((z) =>
        z.Cities.forEach((y: string) => this.cities.push({ Name: y, isChecked: false, isVisible: true }))
      );
    }
    else {
      Object.values(this.collegeResultFilters.StatesAndCities).map((z) =>
        z.Cities.forEach((y: string) => this.cities.push({ Name: y, isChecked: false, isVisible: true })));
    };
    this.filterColleges();
  }


  onCityChange(): void {
    this.filterColleges();
  }

  onTypeChange(): void {
    this.filterColleges();
  }

  onSubTypeChange(): void {
    this.filterColleges();
  }

  filterColleges(): void {
    let selectedStates: string[] = this.collegeResultFilters.StatesAndCities.filter((x) => x.isChecked).map((x) => x.State);
    let selectedCities: string[] = this.cities.filter((x) => x.isChecked).map((x) => x.Name);
    if (selectedStates.length) {
      this.filteredColleges = this.colleges.filter((x) => selectedStates.indexOf(x.State) > -1);

    }
    else {
      this.filteredColleges = this.colleges;
    }
    if (selectedCities.length) {
      this.filteredColleges = this.filteredColleges.filter((x) => selectedCities.indexOf(x.City) > -1);
    }
    let selectedTypes: string[] = this.types.filter((x) => x.isChecked).map((x) => x.Name);
    if (selectedTypes.length) {
      this.filteredColleges = this.filteredColleges.filter((x) => selectedTypes.indexOf(x.Type) > -1);
    }
    let selectedSubTypes: string[] = this.subTypes.filter((x) => x.isChecked).map((x) => x.Name);
    if (selectedSubTypes.length) {
      this.filteredColleges = this.filteredColleges.filter((x) => selectedSubTypes.indexOf(x.SubType) > -1);
    }
  }
}


