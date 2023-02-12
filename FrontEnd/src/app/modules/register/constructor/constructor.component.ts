import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { forbiddenPhoneValidator } from 'src/app/validators/forbidden-phone.directive';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-constructor',
  templateUrl: './constructor.component.html',
  styleUrls: ['./constructor.component.scss']
})
export class ConstructorComponent implements OnInit {

  passwordsMatch = true;
  errNumber = 0;

  public registerConstructorForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required]),
    firstName: new FormControl('', [Validators.required]),
    phoneNumber: new FormControl('', [Validators.required, forbiddenPhoneValidator()]),
    about: new FormControl(),
    checkbox: new FormControl('', [Validators.required]),
  });
  
  constructor(private authService: AuthService,
    private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
  }

  

  get email(): AbstractControl | null{
    return this.registerConstructorForm.get('email');
  }

  get password(): AbstractControl | null{
    return this.registerConstructorForm.get('password');
  }

  get confirmPassword(): AbstractControl | null{
    return this.registerConstructorForm.get('confirmPassword');
  }

  get firstName(): AbstractControl | null{
    return this.registerConstructorForm.get('firstName');
  }

  get about(): AbstractControl | null{
    return this.registerConstructorForm.get('about');
  }


  get checkbox(): AbstractControl | null{
    return this.registerConstructorForm.get('checkbox');
  }

  get phoneNumber(): AbstractControl | null{
    return this.registerConstructorForm.get('phoneNumber');
  }


  register() {
    if(this.registerConstructorForm.value.password != this.registerConstructorForm.value.confirmPassword)
    {
      console.log('Passwords do not match');
      this.passwordsMatch = false;
    }
    else if (this.registerConstructorForm.invalid)
    {
      console.log('Form invalid');
    }
    else {
      this.authService.register(this.registerConstructorForm.value, '/constructor').subscribe({
        next: data => {
            this.router.navigate(['/']);
        },
        error: error => {
            console.error(error.error);
            if(error.error == "User already registered")
            {
              this.errNumber = 1;
            }
            else
            {
              this.errNumber = 2;
            }
        }
    })
    }
  }

}
