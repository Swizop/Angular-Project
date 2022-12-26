import { Injectable } from '@angular/core';
 import { Router } from '@angular/router';

 @Injectable({
   providedIn: 'root'
 })
 export class AuthService {

   constructor(private router: Router) { }

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
 }