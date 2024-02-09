import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor() {}

  // Save the token to localStorage
  setToken(token: string): void {
    localStorage.setItem('token', token);
  }

  // Retrieve the token from localStorage
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Save the user role to localStorage
  setUserRole(role: string): void {
    localStorage.setItem('role', role);
  }

  // Retrieve the user role from localStorage
  getUserRole(): string | null {
    return localStorage.getItem('role');
  }

  // Decode the JWT token to extract payload
  decodeJwtToken(token: string): any {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join(''),
      );

      return JSON.parse(jsonPayload);
    } catch (error) {
      return null;
    }
  }

  // Check if the user is logged in by checking the presence of the token
  isLoggedIn(): boolean {
    const token = this.getToken();
    return token != null;
  }

  // Check if the logged in user is an admin
  isAdmin(): boolean {
    const role = this.getUserRole();
    return role === 'admin';
  }

  // Clear user token and role from localStorage
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
  }
}
