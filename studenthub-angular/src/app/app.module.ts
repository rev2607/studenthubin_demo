import { NgModule } from '@angular/core';
import { BrowserModule, Meta } from '@angular/platform-browser';
//import { AgmCoreModule } from '@agm/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { CommonModule, Location } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header.component';
import { FooterComponent } from './Components/footer/footer.component';
import { HomeComponent } from './Components/home/home.component';
import { HomesearchComponent } from './Components/homesearch/homesearch.component';
import { QuickinfoComponent } from './Components/quickinfo/quickinfo.component';
import { SearchresultsComponent } from './Components/searchresults/searchresults.component';
import { SearchresultsMainComponent } from './Components/searchresults-main/searchresults-main.component';
import { ResultdetailsComponent } from './Components/results/resultdetails/resultdetails.component';
import { TopbarComponent } from './Components/topbar/topbar.component';
import { HomeNewsresourcesComponent } from './Components/home-newsresources/home-newsresources.component';
import { HomeLatestupdatesComponent } from './Components/home-latestupdates/home-latestupdates.component';
import { HomeScholarshipsComponent } from './Components/home-scholarships/home-scholarships.component';
import { HomeTopuniversitiesComponent } from './Components/home-topuniversities/home-topuniversities.component';
import { HomeAbroadtoprankeduniversitiesComponent } from './Components/home-abroadtoprankeduniversities/home-abroadtoprankeduniversities.component';
import { HomeArticlesComponent } from './Components/home-articles/home-articles.component';
import { HomeQuestionsinterviewsloansComponent } from './Components/home-questionsinterviewsloans/home-questionsinterviewsloans.component';
import { CollegesSearchComponent } from './Components/colleges-search/colleges-search.component';
import { CollegeComponent } from './Components/college/college.component';
import { ResultsApInterFirstYearRegularComponent } from './Components/results/results-ap-inter-firstyear-regular/results-ap-inter-firstyear-regular.component';
import { ResultsApInterSecondYearRegularComponent } from './Components/results/results-ap-inter-secondyear-regular/results-ap-inter-secondyear-regular.component';
import { ResultsApEapcetAgricultureMedicalComponent } from './Components/results/results-ap-eapcet-agriculture-medical/results-ap-eapcet-agriculture-medical.component';
import { ResultsApEapcetEngineeringComponent } from './Components/results/results-ap-eapcet-engineering/results-ap-eapcet-engineering.component';
import { ResultsApInterFirstYearVocationalComponent } from './Components/results/results-ap-inter-firstyear-vocational/results-ap-inter-firstyear-vocational.component';
import { ResultsApInterSecondYearVocationalComponent } from './Components/results/results-ap-inter-secondyear-vocational/results-ap-inter-secondyear-vocational.component';
import { ResultsApSSCComponent } from './Components/results/results-ap-ssc/results-ap-ssc.component';
import { ResultsTsEamcetAgricultureMedicalComponent } from './Components/results/results-ts-eamcet-agriculture-medical/results-ts-eamcet-agriculture-medical.component';
import { ResultsTsEamcetEngineeringComponent } from './Components/results/results-ts-eamcet-engineering/results-ts-eamcet-engineering.component';
import { ResultsTsInterFirstYearRegularComponent } from './Components/results/results-ts-inter-firstyear-regular/results-ts-inter-firstyear-regular.component';
import { ResultsTsInterFirstYearVocationalComponent } from './Components/results/results-ts-inter-firstyear-vocational/results-ts-inter-firstyear-vocational.component';
import { ResultsTsInterSecondYearRegularComponent } from './Components/results/results-ts-inter-secondyear-regular/results-ts-inter-secondyear-regular.component';
import { ResultsTsInterSecondYearVocationalComponent } from './Components/results/results-ts-inter-secondyear-vocational/results-ts-inter-secondyear-vocational.component';
import { ResultsTsSSCComponent } from './Components/results/results-ts-ssc/results-ts-ssc.component';
import { ComingSoonComponent } from './Components/coming-soon/coming-soon.component';
import { NewsComponent } from './Components/news/news.component';
import { NewsDetailsComponent } from './Components/news/news-details/news-details.component';
import { ExamNotificationDetailsComponent } from './Components/exam-notification details/exam-notification-details.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    HomesearchComponent,
    QuickinfoComponent,
    SearchresultsComponent,
    SearchresultsMainComponent,
    ResultdetailsComponent,
    TopbarComponent,
    HomeNewsresourcesComponent,
    HomeLatestupdatesComponent,
    HomeScholarshipsComponent,
    HomeTopuniversitiesComponent,
    HomeAbroadtoprankeduniversitiesComponent,
    HomeArticlesComponent,
    HomeQuestionsinterviewsloansComponent,
    ResultsApInterFirstYearRegularComponent,
    ResultsApInterSecondYearRegularComponent,
    ResultsApEapcetAgricultureMedicalComponent,
    ResultsApEapcetEngineeringComponent,
    ResultsApInterFirstYearVocationalComponent,
    ResultsApInterSecondYearVocationalComponent,
    ResultsApSSCComponent,
    ResultsTsSSCComponent,
    ResultsTsInterFirstYearRegularComponent,
    ResultsTsInterSecondYearRegularComponent,
    ResultsTsEamcetAgricultureMedicalComponent,
    ResultsTsEamcetEngineeringComponent,
    ResultsTsInterFirstYearVocationalComponent,
    ResultsTsInterSecondYearVocationalComponent,
    CollegesSearchComponent,
    CollegeComponent,
    ComingSoonComponent,
    NewsComponent,
    NewsDetailsComponent,
    ExamNotificationDetailsComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,  
    // AgmCoreModule.forRoot({
    //   apiKey: 'AIzaSyCAPm9ZrKvVS7SnQ57ItEikVzk5SiQbMmc',
    //   libraries: ['places']
    // }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    DataTablesModule,
    NgxPaginationModule,
    NgxSpinnerModule,
  ],
  providers: [Location],
  bootstrap: [AppComponent],
})
export class AppModule {}
