import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ExamsDetail, SearchResultFilters } from './Model/SearchResultFilters';
import { Observable } from 'rxjs';
import { Constants } from './Model/constants';
import { College, CollegeResults, CollegesResultsFilters } from './Model/CollegeResultFilters';
import { ExamResult } from './Model/ExamResult';
import { BehaviorSubject } from 'rxjs';
import { NewsAlert } from './Model/NewsAlert';

@Injectable({
  providedIn: 'root',
})
export class StudenthubWebApiService {
  private baseUrl = 'https://studenthub.in/webapi/';
  constructor(private httpClient: HttpClient) {}
  private ApiKey = 'd1p7ltgg54jd7nsl1alf';

//events

  private result = new BehaviorSubject<any>(null);
  currentResult = this.result.asObservable();
  private hallTicketNumber = new BehaviorSubject<any>({});
  currenthallTicketNumber = this.hallTicketNumber.asObservable();
  sendResult(result: any) {
    this.result.next(result);
  }

  private metatag = new BehaviorSubject<boolean>(false);
  currentMetatTag = this.metatag.asObservable();

  sendHallTicketNumber(resultdetails: any) {
    this.hallTicketNumber.next(resultdetails);
  }

  updateMetaTag(isUpdate: boolean) {
    this.metatag.next(isUpdate);
  }

  async getResultSearchFilters(): Promise<SearchResultFilters> {
    const httpParams = new HttpParams().append('ApiKey', this.ApiKey);

    return await this.httpClient
      .post<SearchResultFilters>(
        this.baseUrl + 'GetResultSearchFilters',
        null,
        { params: httpParams }
      )
      .toPromise();
  }

  public getResultsSearchResults(
    state: string,
    examType: string,
    year: string
  ): Observable<ExamsDetail[]> {
    let httpParams = new HttpParams();
    httpParams = httpParams.append('ApiKey', this.ApiKey);
    if (state && state != Constants.SELECT_STATE) {
      httpParams = httpParams.append('State', state);
    }
    // if(examId && examId !=Constants.SELECT_EXAM) {
    //   httpParams =  httpParams.append("ExamId", examId);
    // }
    if (year && year != Constants.SELECT_YEAR) {
      httpParams = httpParams.append('Year', year);
    }

    if (examType && examType != Constants.SELECT_EXAM_TYPE) {
      httpParams = httpParams.append('ExamType', examType);
    }
    // return this.httpClient.post<ExamsDetail[]>("http://localhost:49834/webapi/"+"GetResultsSearchResults", null, { params: httpParams });
    return this.httpClient.post<ExamsDetail[]>(
      this.baseUrl + 'GetResultsSearchResults',
      null,
      { params: httpParams }
    );
  }

  public getResultBasedOnHallTicketOrName(
    hallTicketNumber: string,
    name: string,
    examId: string,
    year: string
  ): Observable<any> {
    let httpParams = new HttpParams();
    httpParams = httpParams.append('ApiKey', this.ApiKey);
    if (hallTicketNumber) {
      httpParams = httpParams.append('RequestType', 'HallTicket');
      httpParams = httpParams.append('Value', hallTicketNumber);
    } else {
      httpParams = httpParams.append('RequestType', 'Name');
      httpParams = httpParams.append('Value', name);
    }

    httpParams = httpParams.append('Year', year);
    httpParams = httpParams.append('ExamId', examId);
    return this.httpClient.post<ExamResult>(
      this.baseUrl + 'GetResultsMain',
      null,
      { params: httpParams }
    );
    // return this.httpClient.post<ExamResult>("http://localhost:49834/webapi/"+"GetResultsMain", null, { params: httpParams });
  }

  public getCollegesResults(): Observable<CollegeResults> {
    let data = { ApiKey: this.ApiKey };
    return this.httpClient.get<CollegeResults>(
      this.baseUrl + 'GetColleges',
      { params: data }
    );
  }

  public getCollegeFilters(): Observable<CollegesResultsFilters> {
    let data = { ApiKey: this.ApiKey };
    return this.httpClient.get<CollegesResultsFilters>(
      this.baseUrl+ 'GetCollegesFilters',
      { params: data }
    );
  }

  public getCollegeDetails(id: number): Observable<College> {
    let httpParams = new HttpHeaders();
    let data = { ApiKey: this.ApiKey, id: id };
    httpParams = httpParams.append('ApiKey', this.ApiKey);
    return this.httpClient.get<College>(
      this.baseUrl + 'GetCollegeDetails',
      { params: data }
    );
  }

  public getNewsAlerts(): Observable<NewsAlert[]> {
    let httpParams = new HttpHeaders(); 
    let data = { ApiKey: this.ApiKey};
    httpParams = httpParams.append('ApiKey', this.ApiKey);
    return this.httpClient.get<NewsAlert[]>(
      this.baseUrl + 'GetNewsAlerts',
      { params: data }
    );
  }

  public getLatestNews(NumberOfNews: number): Observable<NewsAlert[]> {
    let httpParams = new HttpHeaders(); 
    let data = { ApiKey: this.ApiKey, NumberOfNews: NumberOfNews};
    httpParams = httpParams.append('ApiKey', this.ApiKey);
    return this.httpClient.get<NewsAlert[]>(
      this.baseUrl +'GetLatestNews',
      { params: data }
    );
  }

  public getNewsDetails(UrlKeyWord: string): Observable<NewsAlert> {
    let httpParams = new HttpHeaders();
    let data = { ApiKey: this.ApiKey, UrlKeyWord:UrlKeyWord  };
    httpParams = httpParams.append('ApiKey', this.ApiKey);
    return this.httpClient.get<NewsAlert>(
      this.baseUrl + 'GetNewsDetails',
      { params: data }
    );
  }

  public getExamNotications(): Observable<Array<NewsAlert>> {
    let httpParams = new HttpHeaders();
    let data = { ApiKey: this.ApiKey };
    httpParams = httpParams.append('ApiKey', this.ApiKey);
    return this.httpClient.get<Array<NewsAlert>>(
      this.baseUrl + 'GetExamNotifications',
      { params: data }
    );
  }
}
