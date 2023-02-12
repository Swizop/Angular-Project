import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public apiUrl = 'https://localhost:44396/api/Account';
  constructor(private router: Router,
    public http: HttpClient) { }

  login(lFV : any) {
    return this.http.post<any>(this.apiUrl + '/login', lFV);
  }

  logout() {
    localStorage.clear();

    // pe viitor: req catre API sa se stearga token-ul

    this.router.navigate(['/']);
  }

  isAuthenticated(): boolean {
    // pe viitor: facem si verificari daca token-ul e valid, daca nu e expirat etc
    var b = !!localStorage.getItem('token');
    return b;
  }

  isConstructor(): boolean {
    var l = localStorage.getItem('roles');
    if(l == null)
      return false;
    
    return (l == 'Constructor');
  }

  isUser(): boolean {
    var l = localStorage.getItem('roles');
    if(l == null)
      return false;
    
    return (l == 'User');
  }

  getUserId() {
    var retval = localStorage.getItem('userId');
    if(retval == null)
      return '';
    return retval;
  }
  register(registerUser: any, bonus: string): Observable<any> {

    return this.http.post<any>(this.apiUrl + '/register' + bonus, registerUser);
  }

  edit_phone(pFV : any) {
    return this.http.put(this.apiUrl + '/edit/phone', pFV);
  }

  deleteAccount() {
    return this.http.delete(this.apiUrl + '/delete');
  }
}
