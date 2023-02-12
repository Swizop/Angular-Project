import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { forbiddenPhoneValidator } from 'src/app/validators/forbidden-phone.directive';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: []
})
export class SettingsComponent implements OnInit {

  errNumber = 0;

  public phoneForm: FormGroup = new FormGroup({
    phoneNumber: new FormControl('', [Validators.required, forbiddenPhoneValidator()])
  });


  constructor(private authService: AuthService, private router: Router) { }

  get phoneNumber(): AbstractControl | null {
    return this.phoneForm.get('phoneNumber');
  }

  ngOnInit(): void {
  }

  editPhone() {
    if (this.phoneForm.invalid) { return; }
    else {
      this.authService.edit_phone(this.phoneForm.value).subscribe({
        next: () => {
          this.errNumber = 2;
        },
        error: (err) => {
          console.error(err);
        }
      })
    }
  }

  deleteAccount() {
    this.authService.deleteAccount().subscribe({
      next: () => {
        this.authService.logout();
      },
      error: (err) => {
        console.error(err);
      }
    })
  }

}
