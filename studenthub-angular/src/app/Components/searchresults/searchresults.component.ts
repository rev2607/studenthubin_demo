import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { of, Subscription } from 'rxjs';
import {
  Exam,
  SearchResultFilters,
  ExamsDetail,
} from '../../Model/SearchResultFilters';
import { StudenthubWebApiService } from '../../studenthub-web-api.service';
import { DataTablesModule, DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-searchresults',
  templateUrl: './searchresults.component.html',
  styleUrls: ['./searchresults.component.css'],
})
export class SearchresultsComponent implements OnInit, OnChanges {
  @Input() public selectedState: string = '';
  @Input() public selectedExamType: string = '';
  //@Input() public selectedExam: string ="";
  @Input() public selectedYear: string = '';
  // @Input() public filterData: SearchResultFilters;
  public examDetails: ExamsDetail[] = [];
  displayColumnLabel(fieldValue: any) {
   // console.log(fieldValue);
    if (fieldValue) {
      return fieldValue; // do something you want to do with value (modifications)
    }
    return 'some default text or value';
  }

  @ViewChild(DataTableDirective) dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private studentHubWebApi: StudenthubWebApiService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'simple_numbers',
      pageLength: 10,
      searching: true,
      processing: true,
    };

    this.filterDataBasedOnSelectedItems();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.filterDataBasedOnSelectedItems();
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Call the dtTrigger to rerender again
      this.dtTrigger.next();
    });
  }
  ngAfterViewInit() {
    this.dtTrigger.next();
  }

  public filterDataBasedOnSelectedItems(): void {
    this.spinner.show();
    this.studentHubWebApi
      .getResultsSearchResults(
        this.selectedState,
        this.selectedExamType,
        this.selectedYear
      )
      .subscribe((examDetails: ExamsDetail[]) => {
        this.examDetails = examDetails;
        this.rerender();
        this.spinner.hide();
      },
        (err) =>{
          this.spinner.hide();
        }
      );
   
  }

  onBtnClick1(e: any) {
    this.studentHubWebApi.sendHallTicketNumber({
      examId: e.rowData.examId,
      year: e.rowData.Year,
      examName: e.rowData.Result,
    });
    this.router.navigate(['/resultdetails']);
  }
}
