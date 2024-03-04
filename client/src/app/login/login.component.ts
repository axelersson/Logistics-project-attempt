import { Component, OnInit } from '@angular/core'; // Import OnInit
import { FormControl, FormGroup } from '@angular/forms';
import { Client, LoginRequest } from '../services/api';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  // Implement OnInit interface
  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  hidePassword: boolean = true;
  faEye = faEye;
  faEyeSlash = faEyeSlash;
  loginError: string | null = null;

  constructor(
    private client: Client,
    private router: Router,
    private authService: AuthService,
  ) {}

  ngOnInit(): void {
    this.checkTokenAndRedirect();
  }

  checkTokenAndRedirect(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/homepage']);
    }
  }

  onLogin(): void {
    const username = this.loginForm.value.username ?? '';
    const password = this.loginForm.value.password ?? '';
    const loginRequest = new LoginRequest({ username, password });

    this.client.login(loginRequest).subscribe(
      (response: any) => {
        const token = response?.token;
        if (token) {
          this.authService.setToken(token);
          const decodedToken = this.authService.decodeJwtToken(token);
          if (decodedToken && decodedToken.role) {
            this.authService.setUserRole(decodedToken.role);
          }
          this.router.navigate(['/homepage']);
        } else {
          console.error('Token not found in response');
          this.loginError = 'Token not found in response';
        }
      },
      (error) => {
        console.error('Login error:', error);
        this.loginError = error.message || 'An error occurred during login.';
      },
    );
  }

  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }
}
