import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Area, AreasResponse } from './api';
import { tap } from 'rxjs/operators';
import { Client } from './api';  // Adjust the import path as necessary

@Injectable({
  providedIn: 'root',
})
export class AreaService {
  private areasSource = new BehaviorSubject<Area[]>([]);
  public areas$ = this.areasSource.asObservable();

  constructor(private client: Client) {}

  fetchAllAreas() {
    this.client.areasGET().subscribe({
      next: (response: any) => {
        if (response && response.Areas) {
          this.areasSource.next(response.Areas);
        } else {
          console.error('The response format is not supported:', response);
        }
      },
      error: (error) => {
        console.error('Error fetching areas:', error);
      },
    });
  }

  fetchAreaById(areaId: string) {
    this.client.areasGET2(areaId).subscribe({
      next: (response: any) => {
        // Process the response as needed
      },
      error: (error) => {
        console.error('Error fetching area details:', error);
      },
    });
  }

  createArea(newArea: Area) {
    this.client.areasPOST(newArea).subscribe({
      next: (response: any) => {
        // Process the response as needed
      },
      error: (error) => {
        console.error('Error creating area:', error);
      },
    });
  }

  updateArea(areaId: string, updatedArea: Area) {
    this.client.areasPUT(areaId, updatedArea).subscribe({
      next: () => {
        // Process the response as needed
      },
      error: (error) => {
        console.error('Error updating area:', error);
      },
    });
  }

  deleteArea(areaId: string): Observable<void> {
    return this.client.areasDELETE(areaId).pipe(
      tap(() => {
        this.refreshAreas();
        console.log(`Area with ID ${areaId} deleted successfully.`);
      }),
    );
  }

  private refreshAreas() {
    this.fetchAllAreas();
  }
}