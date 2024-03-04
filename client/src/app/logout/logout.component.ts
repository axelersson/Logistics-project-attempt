import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css'], // Corrected from styleUrl to styleUrls
})
export class LogoutComponent {
  constructor(private authService: AuthService) {}

  logout(): void {
    this.authService.logout(); // Assuming logout is the correct method name
  }
}
