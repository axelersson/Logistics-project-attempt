import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from '.service/api'; 
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  constructor(private http: HttpClient, @Inject(API_BASE_URL) private baseUrl: string) {}

  getLocations(): Observable<any[]> {
    return this.http.get<any>(`${this.baseUrl}/api/locations`).pipe(
      map(response => response.locations || []) // 假设后端返回的结构是 { locations: [...] }
    );
  }
  
}
