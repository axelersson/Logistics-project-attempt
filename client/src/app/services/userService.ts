import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {
  Client,
  UserRole,
  UpdateUserRoleModel,
  UserIdAndRoleResponseFromUsernameRequest,
  UpdateUserPasswordAndRoleModel,
} from './api'; // Adjust the import path as necessary
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private usernamesSource = new BehaviorSubject<string[]>([]);
  public usernames$ = this.usernamesSource.asObservable(); // Expose the usernames as an Observable
  // Add a new BehaviorSubject for the selected user details
  private selectedUserDetailsSource =
    new BehaviorSubject<UserIdAndRoleResponseFromUsernameRequest | null>(null);
  public selectedUserDetails$ = this.selectedUserDetailsSource.asObservable();

  constructor(private client: Client) {}

  fetchAllUsernames() {
    this.client.getAllUsernames().subscribe({
      next: (response: any) => {
        if (Array.isArray(response)) {
          // If the response is an array, update the BehaviorSubject
          this.usernamesSource.next(response);
        } else if (
          response &&
          response.usernames &&
          Array.isArray(response.usernames)
        ) {
          // Adjust based on actual structure; this is if the response has a 'usernames' property that is an array
          this.usernamesSource.next(response.usernames);
        } else {
          console.error('The response format is not supported:', response);
        }
      },
      error: (error) => {
        console.error('Error fetching usernames:', error);
      },
    });
  }

  // Method to fetch user details by username
  fetchUserDetailsByUsername(username: string) {
    this.client.byUsername(username).subscribe({
      next: (response: any) => {
        this.selectedUserDetailsSource.next(response);
      },
      error: (error) => {
        console.error('Error fetching user details:', error);
        this.selectedUserDetailsSource.next(null); // Reset or handle as needed
      },
    });
  }
  refreshUsernames() {
    // This method should fetch all usernames again or update the local list.
    this.fetchAllUsernames(); // Assuming this method updates the usernames$ observable.
  }
  /// UserService method to delete user by ID
  deleteUserById(userId: string): Observable<void> {
    return this.client.usersDELETE(userId).pipe(
      tap(() => {
        this.refreshUsernames();
        console.log(`User with ID ${userId} deleted successfully.`);
      }),
    );
  }

  updateUserRole(userId: string, role: string): Observable<any> {
    const enumRole = UserRole[role as keyof typeof UserRole];
    const updateModel = new UpdateUserRoleModel({ userId, role: enumRole });
    return this.client.updaterole(userId, updateModel);
  }

  updatePasswordAndRole(
    userId: string,
    role: string,
    password: string,
  ): Observable<any> {
    const enumRole = UserRole[role as keyof typeof UserRole];
    const updateModel = new UpdateUserPasswordAndRoleModel({
      userId, // Might be redundant
      role: enumRole,
      newPassword: password,
    });
    return this.client.updatepasswordandrole(userId, updateModel);
  }
}