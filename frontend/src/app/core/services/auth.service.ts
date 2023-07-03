import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'app/shared/models/user';
import { BehaviorSubject, switchMap } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient) {
      this.isLoggedInSubject.next(localStorage.getItem("authToken") ? true : false)
   }

  public login(user: User): Observable<string> {
      let result = this.http.post(
        "https://localhost:44347/api/Auth/login",
         user,
         { responseType: 'text'}
         );
      return result;
  }

  public getMe(): Observable<string> {
    const jwt = localStorage.getItem('authToken');
    if(!jwt)
      throw new Error('No JWT found');

    return this.http.get(
      "https://localhost:44347/api/Auth",
      {responseType: 'text'}
      );
  }

  public SetLoggedStatus(status: boolean) {
    this.isLoggedInSubject.next(status);
  }
}
