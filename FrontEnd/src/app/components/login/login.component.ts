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
    this.authService.login(this.loginForm.value).subscribe({
      next: data => {
          if(data['token'] == '')
          {
            this.errNumber = 1;
            console.log('no token');
          }
          else
          {
            localStorage.setItem('token', data['token']);
            localStorage.setItem('roles', data['roles']);
            localStorage.setItem('userId', data['id']);
            this.router.navigate(['/']);
          }
      },
      error: error => {
          console.error(error.error);
          this.errNumber = 1;
      }
  })

  }
}
