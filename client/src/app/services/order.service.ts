import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { API_BASE_URL } from './api'; // 确保路径匹配你的文件结构
import { throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http: HttpClient, @Inject(API_BASE_URL) private baseUrl: string) {}

  getOrders(): Observable<any> { 
    const url = `${this.baseUrl}/Orders`;
    return this.http.get<any>(url, {
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    }).pipe(
      map(response => response.orders || []), // make the data into array
      catchError(error => throwError(() => error))
    );
  }

  partialDeliver(orderId: string): Observable<void> {
    let url = `${this.baseUrl}/Orders/PartialDeliver/${orderId}`;
    return this.http.put<void>(url, {}).pipe(
      catchError(error => {
        throw new Error(`Error in partial delivery of order: ${error.message}`);
      })
    );
  }

  deliver(orderId: string): Observable<void> {
    let url = `${this.baseUrl}/Orders/Deliver/${orderId}`;
    return this.http.put<void>(url, {}).pipe(
      catchError(error => {
        throw new Error(`Error in complete delivery of order: ${error.message}`);
      })
    );
  }

}
