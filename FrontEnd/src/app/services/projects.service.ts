import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  public apiUrl = 'https://proiect-angular-1e4df-default-rtdb.firebaseio.com';
  constructor(private http: HttpClient) { }

  newProject(newPostFormValue: any) {
    return this.http.post<any>(this.apiUrl + '/projects.json', newPostFormValue);
  }
}
