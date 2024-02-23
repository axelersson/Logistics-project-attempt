// usercreate.component.ts

import { Component, OnInit } from '@angular/core';
import { User, UserRole } from '../../services/api'; // Assuming User and UserRole are exported from the correct path
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { UserService } from '../../services/userService';
import { UserCreateModel } from '../../services/api';

@Component({
  selector: 'app-usercreate',
  templateUrl: './usercreate.component.html',
  styleUrls: ['./usercreate.component.css'],
})
export class UsercreateComponent implements OnInit {
  user: User = new User(); // Initialize an empty user object
  confirmedPassword: string = '';
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;
  passwordsMatch: boolean = true;
  roles: UserRole[] = [UserRole.Admin, UserRole.User]; // Use UserRole enum from the imported User model
  faEye = faEye;
  faEyeSlash = faEyeSlash;
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {}

  toggleShowPassword(): void {
    this.showPassword = !this.showPassword;
  }

  toggleShowConfirmPassword(): void {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  checkPasswords(): void {
    this.passwordsMatch = this.user.passwordHash === this.confirmedPassword;
  }

  createUser(): void {
    // Reset messages
    this.successMessage = '';
    this.errorMessage = '';

    // Validate username and password
    const usernameValid = /^[a-zA-Z0-9]{5,}$/.test(this.user.username || ''); // At least 5 characters, only letters and numbers
    const passwordValid = (this.user.passwordHash || '').length > 8;

    // Check if passwords match
    this.checkPasswords();

    if (!usernameValid || !passwordValid || !this.passwordsMatch) {
      this.errorMessage =
        'Validation failed. Make sure the username is more than 4 characters, only contains English letters and numbers, the password is longer than 8 characters, and both passwords match.';
      return; // Stop execution if validation fails
    }

    // Assuming UserCreateModel requires initialization
    const newUser = new UserCreateModel();
    newUser.init({
      username: this.user.username,
      password: this.user.passwordHash,
      role: this.user.role,
    });

    // Proceed with API call if validation passes
    this.userService.createUser(newUser).subscribe(
      (response) => {
        this.successMessage = 'User created successfully.';
        this.clearFields();
      },
      (error) => {
        this.errorMessage = 'Failed to create user. Please try again.';
      },
    );
  }

  clearFields(): void {
    this.user = new User(); // Reset user object
    this.confirmedPassword = ''; // Reset confirmed password
    this.passwordsMatch = true; // Reset password match indicator
  }
}
