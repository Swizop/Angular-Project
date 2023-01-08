import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AngularFireAuth } from '@angular/fire/compat/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private router: Router,
    private fireAuth: AngularFireAuth
  ) { }

  login() {
    // DE IMPLEMENTAT

    localStorage.setItem('token', 'value from API');
    this.router.navigate(['/']);

  }

  logout() {
    localStorage.clear();

    // req catre API sa se stearga token-ul

    this.router.navigate(['/']);
  }

  isAuthenticated(): boolean {
    // facem si verificari daca token-ul e valid, daca nu e expirat etc
    var b = !!localStorage.getItem('token');
    return b;
  }

  register() {
    // de implementat
    this.router.navigate(['/login']);
  }
}