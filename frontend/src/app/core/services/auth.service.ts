import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'app/shared/models/user';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient) { }

  public login(user: User): Observable<string> {
      let result = this.http.post(
        "https://localhost:44347/api/Auth/login",
         user,
         { responseType: 'text'}
         );
      return result;
  }

  public getMe(): Observable<string> {
    return this.http.get(
      "https://localhost:44347/api/Auth",
      {responseType: 'text'}
       );
  }

  public SetLoggedStatus(status: boolean) {
    this.isLoggedInSubject.next(status);
  }
}
