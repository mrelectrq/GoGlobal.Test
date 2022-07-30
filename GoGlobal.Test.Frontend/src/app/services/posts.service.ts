import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { FbCreateResponse, IPost } from '../help/interfaces';
import { environment } from '../../environments/environment';
import { catchError, map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class PostsService {
  constructor(private http: HttpClient) {}

  // create(post: Post): Observable<Post> {
  //   return this.http.post(`${environment.fbDbUrl}/cards.json`, post).pipe(
  //     map((response: FbCreateResponse) => {
  //       return {
  //         ...post,
  //         id: response.name,
  //         date: new Date(post.date),
  //       };
  //     })
  //   );
  // }

  searchForRepositories(searchKeyword: string): Observable<any> {
    return this.http
      .get<any>(`http://localhost:5100/api/Repository?repositoryName=${searchKeyword}`)
      .pipe(
        map((data) => data),
        catchError((err) => {
          console.log('Handling error locally and rethrowing it...', err);
          return throwError(err);
        })
      );
  }

  addBookmark(newBookmark: IPost): Observable<any> {
    return this.http
      .post<any>(`http://localhost:5100/api/Repository`, newBookmark)
      .pipe(
        map((data) => data),
        catchError((err) => {
          console.log('Handling error locally and rethrowing it...', err);
          return throwError(err);
        })
      );
  }

  deleteBookmark(bookmarkId: string) {
    return this.http
      .delete<any>(`http://localhost:5100/bookmark?repositoryId=${bookmarkId}`)
      .pipe(
        map((data) => data),
        catchError((err) => {
          console.log('Handling error locally and rethrowing it...', err);
          return throwError(err);
        })
      );
  }

  getAllBookmarks(): Observable<any> {
    return this.http.get<any>(`http://localhost:5100/bookmark`).pipe(
      map((data) => data),
      catchError((err) => {
        console.log('Handling error locally and rethrowing it...', err);
        return throwError(err);
      })
    );
  }
}
//        `http://localhost:5100/api/Repository?repositoryName=${searchKeyword}`
