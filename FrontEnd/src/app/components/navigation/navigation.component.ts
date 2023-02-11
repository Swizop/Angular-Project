import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { DataService } from 'src/app/services/data.service';
import { ProjectsService } from 'src/app/services/projects.service';
import { locationValidator } from 'src/app/validators/location-format.directive';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  errNumber = 0;
  public newPostForm: FormGroup = new FormGroup({
    title: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    location: new FormControl('', [Validators.required, locationValidator()])
  });

  constructor(
    private authService: AuthService,
    private data: DataService,
    private projects: ProjectsService,
    private router: Router
  ) { }

  get title(): AbstractControl | null {
    return this.newPostForm.get('title');
  }

  get description(): AbstractControl | null {
    return this.newPostForm.get('description');
  }

  get location(): AbstractControl | null {
    return this.newPostForm.get('location');
  }

  ngOnInit(): void {
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  logout() {
    this.authService.logout();
  }

  newRegisterAs(s: string) {
    this.data.changeRegisterAs(s); // service folosit pt comunicare intre navbar si register page
  }

  add_post() {
    if(this.newPostForm.invalid)
     {
       this.errNumber = 1;
     }
     else
     {
       this.projects.newProject(this.newPostForm.value).subscribe({
         next: () => {
           console.log("Succes!");
           this.errNumber = 0;
           this.router.navigate(['/']);
         },
         error: err => {
           console.error(err.error);
           this.errNumber = 2;
         }
       })
     }
  }
}