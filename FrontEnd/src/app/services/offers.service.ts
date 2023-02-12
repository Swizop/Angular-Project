import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OffersService {

  public apiUrl = 'https://localhost:44396/api/Offer';
  constructor(private http: HttpClient) { }

  new_offer(oFV : any) {
    return this.http.post<any>(this.apiUrl + '/new', oFV);
  }

  getOffersForProject(id : any) : Observable<any> {
    return this.http.get(this.apiUrl + '/getByProject/' + id);
  }

  getAcceptedOffers() : Observable<any> {
    return this.http.get(this.apiUrl + '/getAccepted');
  }

  acceptOffer(consId : any, projId : any) : Observable<any> {
    return this.http.put(this.apiUrl + '/edit/' + projId + '/' + consId, {"accepted" : `1`});
  }

  deleteOffer(consId : any, projId : any) : Observable<any> {
    return this.http.delete(this.apiUrl + '/delete/' + projId + '/' + consId);
  }
}
