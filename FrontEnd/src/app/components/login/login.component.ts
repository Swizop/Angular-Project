import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup = new FormGroup({
    email: new FormControl(),
    password: new FormControl(),
    checkbox: new FormControl(),
  });

  constructor(private authService: AuthService) { }

  //getters
  get email(): AbstractControl | null{
    return this.loginForm.get('email');
  }

  get password(): AbstractControl | null{
    return this.loginForm.get('password');
  }

  get checkbox(): AbstractControl | null{
    return this.loginForm.get('checkbox');
  }

  ngOnInit(): void {
  }

  login() {
    console.log(this.loginForm.value);
    this.authService.login();
  }

}