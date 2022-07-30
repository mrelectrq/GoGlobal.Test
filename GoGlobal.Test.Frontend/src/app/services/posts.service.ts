import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { FbCreateResponse, Post } from '../help/interfaces';
import { environment } from '../../environments/environment';
import { catchError, map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class PostsService {
  constructor(private http: HttpClient) {}

  create(post: Post): Observable<Post> {
    return this.http.post(`${environment.fbDbUrl}/cards.json`, post).pipe(
      map((response: FbCreateResponse) => {
        return {
          ...post,
          id: response.name,
          date: new Date(post.date),
        };
      })
    );
  }

  getAll(): Observable<any> {
    return this.http
      .get<any>(
        `https://api.github.com/search/repositories?q=YOUR_SEARCH_KEYWORD`
      )
      .pipe(
        map((data) => data),
        catchError((err) => {
          console.log('Handling error locally and rethrowing it...', err);
          return throwError(err);
        })
      );
  }

  getWeatherMap(city: string): Observable<any> {
    return this.http
      .get<any>(
        `https://api.openweathermap.org/data/2.5/weather?q=${city}&units=metric&lang=en&APPID=${environment.apiKey}`
      )
      .pipe(
        map((data) => data),
        catchError((err) => {
          console.log('Handling error locally and rethrowing it...', err);
          return throwError(err);
        })
      );
  }
}
