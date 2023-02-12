import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { forbiddenPhoneValidator } from 'src/app/validators/forbidden-phone.directive';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  passwordsMatch = true;
  errNumber = 0;

  public registerForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    phoneNumber: new FormControl('', [Validators.required, forbiddenPhoneValidator()]),
    checkbox: new FormControl('', [Validators.required]),
  });

  constructor(private authService: AuthService,
    private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
  }



  get email(): AbstractControl | null {
    return this.registerForm.get('email');
  }

  get password(): AbstractControl | null {
    return this.registerForm.get('password');
  }

  get confirmPassword(): AbstractControl | null {
    return this.registerForm.get('confirmPassword');
  }

  get lastName(): AbstractControl | null {
    return this.registerForm.get('lastName');
  }

  get firstName(): AbstractControl | null {
    return this.registerForm.get('firstName');
  }


  get checkbox(): AbstractControl | null {
    return this.registerForm.get('checkbox');
  }

  get phoneNumber(): AbstractControl | null {
    return this.registerForm.get('phoneNumber');
  }


  register2() {
    if (this.registerForm.value.password != this.registerForm.value.confirmPassword) {
      console.log('Passwords do not match');
      this.passwordsMatch = false;
    }
    else if (this.registerForm.invalid) {
      console.log('Form invalid');
    }
    else {
      this.authService.register(this.registerForm.value, '').subscribe({
        next: data => {
          this.router.navigate(['/']);
        },
        error: error => {
          console.error(error.error);
          if (error.error == "User already registered") {
            this.errNumber = 1;
          }
          else {
            this.errNumber = 2;
          }
        }
      })
    }
  }
}
