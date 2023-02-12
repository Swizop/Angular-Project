import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectsService } from 'src/app/services/projects.service';
import { locationValidator } from 'src/app/validators/location-format.directive';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  @Input()
  project!: { [x: string]: any; };

  @Output() finalizeEditEvent = new EventEmitter<boolean>();

  errNumber = 0;
  public editPostForm: FormGroup = new FormGroup({
    title: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    location: new FormControl('', [Validators.required, locationValidator()])
  });
  constructor(private projects: ProjectsService) { }

  get title(): AbstractControl | null {
    return this.editPostForm.get('title');
  }

  get description(): AbstractControl | null {
    return this.editPostForm.get('description');
  }

  get location(): AbstractControl | null {
    return this.editPostForm.get('location');
  }

  ngOnInit(): void {              // 'injectam' proiectul in form-ul nostru, sa aiba valori default in UI
    this.editPostForm.patchValue({
      title: this.project["title"],
      description: this.project["description"],
      location: this.project["location"]
    });
  }

  closeEdit() {
    this.finalizeEditEvent.emit(false);
  }

  submitEdit() {
    if (this.editPostForm.invalid) {
      this.errNumber = 1;
    }
    else {
      this.projects.edit_project(this.editPostForm.value, this.project["id"]).subscribe({
        next: () => {
          console.log("Succes!");
          this.errNumber = 0;
          this.finalizeEditEvent.emit(false);
        },
        error: err => {
          console.error(err.error);
          this.errNumber = 2;
        }
      })
    }
  }
}
