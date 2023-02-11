import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  errNumber = 0;

  public loginForm: FormGroup = new FormGroup({
    email: new FormControl(),
    password: new FormControl()
  });

  constructor(private authService: AuthService, private router: Router) { }

  //getters
  get email(): AbstractControl | null{
    return this.loginForm.get('email');
  }

  get password(): AbstractControl | null{
    return this.loginForm.get('password');
  }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(String(this.email?.value), String(this.password?.value)).then(
      credential => {
        const token = credential.user?.getIdToken().then(idToken => {
           if(idToken == '')
             this.errNumber = 1;
           else
           {
             localStorage.setItem('token', idToken);
             this.router.navigate(['/']);
           }
        })
      }
    )
    .catch(error => {
      console.error(error.error);
      this.errNumber = 1;
    });
  }

}