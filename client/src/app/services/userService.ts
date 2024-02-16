import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Client } from './api'; // Adjust the import path as necessary

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private usernamesSource = new BehaviorSubject<string[]>([]);
  public usernames$ = this.usernamesSource.asObservable(); // Expose the usernames as an Observable

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

  // Optional: Method to get current value of usernames synchronously
  getCurrentUsernames(): string[] {
    return this.usernamesSource.getValue();
  }
}
