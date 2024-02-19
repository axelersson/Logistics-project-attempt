import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/userService';
import { UserIdAndRoleResponseFromUsernameRequest } from '../../services/api';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-userdetail',
  templateUrl: './userdetail.component.html',
  styleUrls: ['./userdetail.component.css'],
})
export class UserdetailComponent implements OnInit {
  selectedUsername: string = '';
  selectedUserPassword: string = '';
  selectedUserRole: string = '';
  selectedUserId: string = '';
  confirmedPassword: string = '';
  originalUserRole: string = '';
  passwordsMatch: boolean = true;
  userDetailsFetched: boolean = false;
  passwordError: string = '';
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;
  faEye = faEye;
  faEyeSlash = faEyeSlash;
  updateSuccessMessage: string = '';

  usernames: string[] = [];
  roles: string[] = ['Admin', 'User'];
  userDetails: UserIdAndRoleResponseFromUsernameRequest | null = null;

  constructor(
    private userService: UserService,
    public dialog: MatDialog,
    private router: Router,
  ) {}

  ngOnInit() {
    this.userService.usernames$.subscribe(
      (usernames) => (this.usernames = usernames),
    );
    this.userService.fetchAllUsernames();
  }

  checkPasswords(): void {
    this.passwordsMatch = this.selectedUserPassword === this.confirmedPassword;
  }

  toggleShowPassword() {
    this.showPassword = !this.showPassword;
  }

  toggleShowConfirmPassword() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  onUsernameSelect(): void {
    this.userDetailsFetched = false;
    this.originalUserRole = ''; // Reset originalUserRole on new selection
  }

  selectUser(): void {
    this.originalUserRole = this.userDetails?.role || '';
    if (this.selectedUsername) {
      this.userService.fetchUserDetailsByUsername(this.selectedUsername);
      // Assuming fetchUserDetailsByUsername updates selectedUserDetails$ Observable
      this.userService.selectedUserDetails$.subscribe(
        (details: UserIdAndRoleResponseFromUsernameRequest | null) => {
          if (details) {
            this.userDetails = details;
            this.selectedUserRole = details.role || ''; // Provide a default if undefined
            this.userDetailsFetched = true;
            if (details.userId) {
              this.selectedUserId = details.userId;
            }
          }
        },
      );
    }
  }

  createUser() {
    this.router.navigate(['/usercreate']);
  }
  editUser() {
    this.passwordError = ''; // Reset any existing error messages
    this.updateSuccessMessage = ''; // Reset the success message

    if (this.userDetails && this.selectedUsername) {
      const userId = this.userDetails.userId; // Assuming `userDetails` includes `userId`

      if (
        this.selectedUserRole !== this.originalUserRole &&
        !this.selectedUserPassword
      ) {
        // Only role is changed
        this.userService
          .updateUserRole(this.selectedUserId, this.selectedUserRole)
          .subscribe({
            next: () => {
              console.log('User role updated successfully');
              // Optionally, refresh part of the UI or navigate
              this.selectUser(); // Refresh the list of usernames
              this.router.navigate(['/userdetail']); // Navigate to the user selection page
            },
            error: (error) => {
              console.error('Error updating user role', error);
              this.passwordError = 'Failed to update user role.';
            },
          });
      } else if (
        this.passwordsMatch &&
        this.selectedUserPassword &&
        this.selectedUserPassword.length >= 8
      ) {
        // Password and possibly role are updated
        this.userService
          .updatePasswordAndRole(
            this.selectedUserId,
            this.selectedUserRole,
            this.selectedUserPassword,
          )
          .subscribe({
            next: () => {
              console.log('User password and role updated successfully');
              // Optionally, refresh part of the UI or navigate
              this.selectedUserPassword = '';
              this.confirmedPassword = '';
              this.updateSuccessMessage = 'Password change was successful.';
              this.selectUser(); // Refresh the list of usernames
              this.router.navigate(['/userdetail']); // Navigate to the user selection page
            },
            error: (error) => {
              console.error('Error updating user password and role', error);
              this.passwordError = 'Failed to update user password and role.';
            },
          });
      } else {
        // Handle other potential cases, such as password not meeting requirements
        if (!this.passwordsMatch) {
          this.passwordError = 'Passwords do not match.';
        } else if (
          this.selectedUserPassword &&
          this.selectedUserPassword.length < 8
        ) {
          this.passwordError = 'Password must be at least 8 characters long.';
        }
      }
    } else {
      // Handle case where user details might not be fully specified
      this.passwordError = 'User details are not specified or incomplete.';
    }
  }

  confirmDelete(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px',
      data: { message: 'Are you sure you want to delete this user?' },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result && this.userDetails?.userId) {
        // Ensure userId is defined before attempting to delete
        this.userService.deleteUserById(this.userDetails.userId).subscribe({
          next: () => {
            console.log(`User ${this.selectedUsername} has been deleted.`);
            this.userService.refreshUsernames(); // Refresh the list of usernames
            this.router.navigate(['/userdetail']); // Navigate to the user selection page
          },
          error: (error: any) => {
            // Specify type to address TS7006 error
            console.error(
              `Failed to delete user ${this.selectedUsername}:`,
              error,
            );
            // Handle error
          },
        });
      }
    });
  }
}
