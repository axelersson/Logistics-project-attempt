import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  isAdmin: boolean = false; 
  constructor(private authService: AuthService) {}

  ngOninit(): void{
    this.isAdmin = this.authService.isAdmin();
  }
}
