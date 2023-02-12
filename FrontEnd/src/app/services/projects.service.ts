import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  public apiUrl = 'https://localhost:44396/api/Project';
  constructor(private http: HttpClient) { }

  new_project(pFV : any) {
    return this.http.post<any>(this.apiUrl + '/new', pFV);
  }

  edit_project(eFV : any, id : any) {
    return this.http.put(this.apiUrl + '/edit/' + id, eFV);
  }

  getAvailableProjects(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  deleteProject(id: any): Observable<any> {
    return this.http.delete(this.apiUrl + '/delete/' + id);
  }
}
