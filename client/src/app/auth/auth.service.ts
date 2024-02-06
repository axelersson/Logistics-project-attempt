// Inside auth.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private loginUrl = 'your_backend_api_url_for_login';

  constructor(private http: HttpClient) {}

  login(userCredentials: { username: string; password: string }) {
    return this.http.post(this.loginUrl, userCredentials);
  }
}
