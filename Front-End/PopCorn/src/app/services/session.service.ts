import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Session } from '../models/session';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  create(url: string, session: Session): Observable<any> {
    return this._httpClient.post<any>(url, session);
  }

  getSessionById(url: string, id: number): Observable<any> {
    var newUrl = `${url}/${id}`;
    return this._httpClient.get<any>(newUrl);
  }

  getSessions(url: string): Observable<any> {
    return this._httpClient.get<any>(url);
  }

  delete(url: string, id: number, forceDelete: boolean): Observable<any> {
    var newUrl = `${url}/${id}/${forceDelete}`;
    return this._httpClient.delete<any>(newUrl);
  }

  limitedDate() {
    var dtToday = new Date();
    var month = (dtToday.getMonth() + 1).toString();     // getMonth() is zero-based
    var day = dtToday.getDate().toString();
    var year = dtToday.getFullYear();
    if (parseInt(month) < 10)
      month = `0${month}`;
    if (parseInt(day) < 10)
      day = `0${day}`;

    var maxDate = year + '-' + month + '-' + day;

    return maxDate;
  }
}
