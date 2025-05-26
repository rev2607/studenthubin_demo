import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { SearchresultsMainComponent } from './Components/searchresults-main/searchresults-main.component';
import { ResultdetailsComponent } from './Components/results/resultdetails/resultdetails.component';
import { CollegesSearchComponent } from './Components/colleges-search/colleges-search.component';
import { CollegeComponent } from './Components/college/college.component';
import { ComingSoonComponent } from './Components/coming-soon/coming-soon.component';
import { NewsComponent } from './Components/news/news.component';
import { NewsDetailsComponent } from './Components/news/news-details/news-details.component';
import { ExamNotificationDetailsComponent } from './Components/exam-notification details/exam-notification-details.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'results', component: SearchresultsMainComponent },
  { path: 'home', component: HomeComponent },
  // { path: 'resultdetails/:url', component: ResultdetailsComponent },
  { path: 'resultdetails', component: ResultdetailsComponent },
  { path: 'colleges', component: CollegesSearchComponent },
  { path: 'colleges/:id', component: CollegeComponent },
  // { path: 'colleges', component: ComingSoonComponent },
  // { path: 'college-details/:id', component: ComingSoonComponent },
  { path: 'study-abroad', component: ComingSoonComponent },
  { path: 'courses', component: ComingSoonComponent },
  { path: 'reviews', component: ComingSoonComponent },
  { path: 'exams', component: ComingSoonComponent },
  { path: 'scholarships', component: ComingSoonComponent },
  { path: 'training-institutes', component: ComingSoonComponent },
  { path: 'news', component: NewsComponent },
  { path: 'news/:urlkeyword', component: NewsDetailsComponent },
  { path: 'exam-notifications/:urlkeyword', component: ExamNotificationDetailsComponent}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
export const routingComponents = [
  HomeComponent,
  SearchresultsMainComponent,
  ResultdetailsComponent,
];
