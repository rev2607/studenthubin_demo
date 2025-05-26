import { StudenthubWebApiService } from 'src/app/studenthub-web-api.service';
import { Component } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,private metaTagService: Meta, private titleService: Title, 
    private studentHubWebService: StudenthubWebApiService){
    
  }
  title = 'studenthub-angular';

  ngOnInit() {
 //this.metaTagService.addTag({nam: 'description', content: 'student hub app component'});
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
    )
      .subscribe(() => {
 
        var rt = this.getChild(this.activatedRoute)
 
        rt.data.subscribe((data: any) => {
          this.studentHubWebService.updateMetaTag(true);
        //   this.titleService.setTitle(data.title)
 
        //   if (data.descrption) {
        //     this.metaTagService.updateTag({ name: 'description', content: data.descrption })
        //   } else {
        //     this.metaTagService.removeTag("name='description'")
        //   }
        // })
 
      })
    });
 
  }

 
  getChild(activatedRoute: ActivatedRoute): any {
    if (activatedRoute.firstChild) {
      return this.getChild(activatedRoute.firstChild);
    } else {
      return activatedRoute;
    }
 
  }
}
 
