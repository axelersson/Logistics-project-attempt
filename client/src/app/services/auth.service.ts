import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // Initialize a BehaviorSubject with the initial login status
  private loggedInStatus = new BehaviorSubject<boolean>(this.isLoggedIn());

  constructor(private router: Router) {}

  // Observable for components to subscribe to for login status changes
  get isLoggedInObservable() {
    return this.loggedInStatus.asObservable();
  }

  // Save the token to localStorage and update the login status
  setToken(token: string): void {
    localStorage.setItem('token', token);
    this.loggedInStatus.next(true); // Notify subscribers of the change
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
    // console.log(role)
    return role === 'Admin';
  }

  // Clear user token and role from localStorage and update login status
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.loggedInStatus.next(false); // Notify subscribers of the change
    this.router.navigate(['/login']);
  }
  // Get user ID from token
  getUserId(): string | null {
    const token = this.getToken();

    if (token) {
      const payload = this.decodeJwtToken(token);
      if (payload && payload.nameid) {
        return payload.nameid;
      }
    }
    return null;
  }
}
