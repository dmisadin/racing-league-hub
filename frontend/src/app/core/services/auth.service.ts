import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { registerUser } from 'app/shared/models/user/RegisterUser';
import { User } from 'app/shared/models/user/User';
import { BehaviorSubject, switchMap } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:44347/api/Auth';
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  private username = new BehaviorSubject<string>('John Doe');

  username$ = this.username.asObservable();
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient) {
    this.isLoggedInSubject.next(
      localStorage.getItem('authToken') ? true : false
    );

    const name = localStorage.getItem('username');
    if (name) this.username.next(name);
  }

  public login(user: User): Observable<string> {
    let result = this.http.post(this.baseUrl + '/login', user, {
      responseType: 'text',
    });
    return result;
  }

  public getMe(): Observable<string> {
    const jwt = localStorage.getItem('authToken');
    if (!jwt) throw new Error('No JWT found');

    return this.http.get(this.baseUrl, {
      responseType: 'text',
    });
  }

  public register(registerUser: registerUser): any {
    let result = this.http.post<any>(this.baseUrl + '/register', registerUser);
    return result;
  }

  public setLoggedStatus(status: boolean) {
    this.isLoggedInSubject.next(status);
  }

  public setUsername(name: string) {
    this.username.next(name);
  }
}
