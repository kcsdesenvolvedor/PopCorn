
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { Global } from '../../shared/Global';
import { Observable, BehaviorSubject } from 'rxjs';
import { UserAccess } from '../models/userAcess';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<any>;
  public currentUser: Observable<any>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): any {
    return this.currentUserSubject.value;
  }

  login(model: UserAccess, url: string){
    return this.http.post(url, model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSubject.next(user);
          }
        }
        )
      )
  }

  logedIn() {
    const user = localStorage.getItem('user');
    return !!user;
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSubject.next(null);
    document.location.assign(`${Global.BASE_URL_LOCAL}/login`);
  }

}
