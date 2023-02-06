import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { DataService } from 'src/app/services/data.service';
import { ProjectsService } from 'src/app/services/projects.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  errNumber = 0;
  public newPostForm: FormGroup = new FormGroup({
    title: new FormControl(),
    description: new FormControl(),
    location: new FormControl()
  });

  constructor(
    private authService: AuthService,
    private data: DataService,
    private projects: ProjectsService
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
    // this.projects.newProject(this.newPostForm.value).subscribe({

    // })
  }
}