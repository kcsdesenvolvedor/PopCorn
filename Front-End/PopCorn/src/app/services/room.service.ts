import { Observable } from 'rxjs';
import { Room } from '../models/room';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Global } from 'src/shared/Global';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  getRoomById(url: string, id: number): Observable<any> {
    const newUrl = `${url}/${id}`;
    return this._httpClient.get(newUrl);
  }

  getRooms(url: string): Observable<any> {
    return this._httpClient.get<any>(url);
  }

  getRoomsAlls(url: string): Observable<any> {
    return this._httpClient.get<any>(url);
  }

  getRoomsBySession(registerForm: FormGroup): Observable<any>{
    let date = registerForm.controls['sessionDate'].value;
    let start = registerForm.controls['startTime'].value;
    let movie = registerForm.controls['movieId'].value;
    let url = `${Global.BASE_URL_API}/room?dateSession=${date}&startTime=${start}&movieId=${movie}`;

    

    return this._httpClient.get<any>(url);
  }

}
