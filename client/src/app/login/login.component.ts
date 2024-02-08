import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Client, LoginRequest } from '../services/api';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  faEye = faEye;
  faEyeSlash = faEyeSlash;
  hidePassword = true;

  constructor(
    private client: Client,
    private router: Router,
  ) {}

  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }

  // Add a property for storing the login error message
  loginError: string = '';

  onLogin(): void {
    const username = this.loginForm.value.username ?? '';
    const password = this.loginForm.value.password ?? '';

    const loginRequest = new LoginRequest({ username, password });

    this.client
      .login(loginRequest)
      .pipe(
        map(() => {
          // On successful login, redirect to /dashboard
          this.router.navigate(['/dashboard']);
        }),
        catchError((error) => {
          // On login failure, set loginError message
          this.loginError = 'Login failed. Please try again.';
          // Optionally, log the error or handle it as needed
          console.error('Login failed:', error);
          return of(null); // Ensures the stream continues
        }),
      )
      .subscribe();
  }
}
