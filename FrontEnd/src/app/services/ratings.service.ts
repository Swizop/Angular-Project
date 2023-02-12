import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RatingsService {

  public apiUrl = 'https://localhost:44396/api/Rating';

  constructor(private http: HttpClient) { }

  send_rating(sFV : any, id : any) {
    return this.http.put(this.apiUrl + '/' + id + '/' + sFV.nrStars, sFV);
  }
}
