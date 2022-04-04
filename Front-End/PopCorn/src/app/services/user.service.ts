
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  Create(url: string, user: User): Observable<any> {
    return this.httpClient.post<any>(url, user);
  }

  GetUsers(url: string): Observable<any> {
    return this.httpClient.get<any>(url);
  }

  Update(url: string, user: User): Observable<any> {
    return this.httpClient.put<any>(url, user);
  }

  Delete(url: string, id: number): Observable<any> {
    let newUrl = `${url}/${id}`;
    return this.httpClient.delete<any>(newUrl);
  }

}
