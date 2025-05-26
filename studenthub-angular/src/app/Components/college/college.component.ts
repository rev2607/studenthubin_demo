import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { College } from '../../Model/CollegeResultFilters';
import { StudenthubWebApiService } from '../../studenthub-web-api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { NewsAlert } from 'src/app/Model/NewsAlert';

@Component({
  selector: 'college-details',
  templateUrl: './college.component.html',
  styleUrls: ['./college.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class CollegeComponent implements OnInit {
  private coll_ID: number;
  public latitude: number;
  public longitude: number;
  public zoom: number;
  public college: College;
  // public selectedCourse: string = '';
  // public selectedCourseDetails: string;

  newsAlerts: Array<NewsAlert> = [];

  constructor(
    private route: ActivatedRoute,
    private studenthubWebApiService: StudenthubWebApiService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    // colleg details
    this.spinner.show();
    this.route.paramMap.subscribe((paramMap) => {
      this.coll_ID = Number(paramMap.get('id'));
      this.studenthubWebApiService.getCollegeDetails(this.coll_ID).subscribe(
        (x) => {
          this.college = x;
          this.spinner.hide();
        },
        (err) => {
          this.spinner.hide();
          console.log(err);
        }
      );
    });

    // news alerts
    this.studenthubWebApiService.getNewsAlerts().subscribe(
      (x: any) => {
        this.newsAlerts = x;
      },
      (err) => {
        console.log(err);
      }
    );

    //this.getGeoLocation("Near Katpadi Road, Vellore, Tamil Nadu 632014");
    //this.setCurrentLocation();
  }

  // private setCurrentLocation() {
  //   if ('geolocation' in navigator) {
  //     navigator.geolocation.getCurrentPosition((position) => {
  //       this.latitude = position.coords.latitude;
  //       this.longitude = position.coords.longitude;
  //       this.zoom = 15;
  //     });
  //   }
  // }

  // public selectChange(): void {
  //   //const courseDetails = this.college.Degree_4Years.split("<p>");
  //   if (this.selectedCourse == "B.E/B.Tech") {
  //     this.selectedCourseDetails = this.college.Degree_4Years;
  //   }
  //   else if (this.selectedCourse == "B.Tech + M.Tech(Dual Degree)") {
  //     this.selectedCourseDetails = this.college.Degree_5Years;
  //   }
  //   else {
  //     this.selectedCourseDetails = "";
  //   }
  //   //this.college.Degree_4Years.match(/(?<=<p>\s+).*?(?=\s+<p>)/gs);
  // }

  scroll(el: HTMLElement) {
    el.scrollIntoView({ behavior: 'smooth' });
  }

  // getGeoLocation(address: string): Observable<any> {
  //   console.log('Getting address: ', address);
  //   let geocoder = new google.maps.Geocoder();
  //   return Observable.create((observer:any) => {
  //       geocoder.geocode({
  //           'address': address
  //       }, (results: any, status:any) => {
  //           if (status == google.maps.GeocoderStatus.OK) {
  //               observer.next(results[0].geometry.location);
  //               observer.complete();
  //               console.log(results[0].geometry.location);
  //           } else {
  //               console.log('Error: ', results, ' & Status: ', status);
  //               observer.error();
  //           }
  //       });
  //   });
  //}
}
