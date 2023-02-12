import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ProjectsService } from 'src/app/services/projects.service';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.scss']
})
export class FeedComponent implements OnInit {

  allProjects = [];
  toggleEdit = false;
  toggleOffer = false;

  injectProject = {};

  constructor(private projectsService: ProjectsService,
    public authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.projectsService.getAvailableProjects().subscribe({
      next: result => {
        // console.log(result);
        this.allProjects = result;
      },
      error: err => {
        console.error(err.error);
      }
    })
  }

  delete(id: any) {
    this.projectsService.deleteProject(id).subscribe({
      next: () => {
        console.log("Succes!");
        window.location.reload();
      },
      error: () => {
        console.log("Eroare!");
      }
    })
  }

  edit(project: any) {
    this.injectProject = project;
    this.toggleEdit = true;
  }

  offer(project: any) {
    this.injectProject = project;
    this.toggleOffer = true;
  }

  finishEditing(b: boolean) {
    this.toggleEdit = b;
  }

  finishOffering(b: boolean) {
    this.toggleOffer = b;
  }

  goToProjectOffers(id: any) {
    this.router.navigate(['/project', id]);
  }
}
