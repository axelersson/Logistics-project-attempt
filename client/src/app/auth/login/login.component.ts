// Inside login.component.ts

import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../auth.service';

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

  constructor(private authService: AuthService) {}

  onLogin() {
    if (this.loginForm.valid) {
      this.authService
        .login({
          username: this.loginForm.value.username!,
          password: this.loginForm.value.password!,
        })
        .subscribe(
          (response) => {
            console.log(response);
            // Handle successful login here
          },
          (error) => {
            console.error(error);
            // Handle login error here
          },
        );
    }
  }
}
