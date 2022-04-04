import { Movie } from '../models/movie';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  jsonImage: object = {};

  constructor(private httpClient: HttpClient) { }

  Create(url: string, movie: Movie): Observable<any> {
    return this.httpClient.post<any>(url, movie);
  }

  GetMovieById(id: number, url: string): Observable<any> {
    let newUrl = `${url}/${id}`;
    return this.httpClient.get<any>(newUrl);
  }

  GetMovies(url: string): Observable<any> {
    return this.httpClient.get<any>(url);
  }

  Update(url: string, movie: Movie): Observable<any> {
    return this.httpClient.put<any>(url, movie);
  }

  Delete(url: string, id: number): Observable<any> {
    let newUrl = `${url}/${id}`;
    return this.httpClient.delete<any>(newUrl);
  }

  mountImage(name: string, base64: string) {

    return this.jsonImage = {
      name: name,
      base64: base64
    }
  }
}
