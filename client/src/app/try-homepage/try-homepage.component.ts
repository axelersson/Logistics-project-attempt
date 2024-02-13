import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-try-homepage',
  templateUrl: './try-homepage.component.html',
  styleUrl: './try-homepage.component.css'
})
export class TryHomepageComponent {
  isAdmin: boolean = false; 
  constructor(private authService: AuthService) {}

  ngOninit(): void{
    this.isAdmin = this.authService.isAdmin();
  }
}
