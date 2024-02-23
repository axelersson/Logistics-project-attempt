import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_BASE_URL } from './api';
import { map } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

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

  deleteLocation(locationId: string): Observable<void> {
    let url = `${this.baseUrl}/api/Locations/${encodeURIComponent(locationId)}`;
    return this.http.delete<void>(url); // 使用 Angular HttpClient 的 delete 方法
}

createLocation(location: any): Observable<any> {
  const url = `${this.baseUrl}/api/Locations`;
  return this.http.post<any>(url, location, {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }).pipe(
    catchError((error: HttpErrorResponse) => {
      // 捕获并转换错误
      let errorMessage = 'An unknown error occurred';
      if (error.error instanceof ErrorEvent) {
        // 客户端侧或网络错误
        errorMessage = `An error occurred: ${error.error.message}`;
      } else {
        // 后端返回一个不成功的响应代码。
        // 响应主体可能包含有关错误的线索
        if (error.status === 400) {
          errorMessage = error.error; // 使用后端提供的错误信息
        }
      }
      return throwError(() => new Error(errorMessage));
    })
  );
}

updateLocation(locationId: string, location: any): Observable<void> {
  const url = `${this.baseUrl}/api/Locations/${encodeURIComponent(locationId)}`;
  return this.http.put<void>(url, location, {
      headers: new HttpHeaders({
          'Content-Type': 'application/json',
      }),
  });
}

  
}
