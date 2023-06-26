import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from 'app/models/user';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

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
}
