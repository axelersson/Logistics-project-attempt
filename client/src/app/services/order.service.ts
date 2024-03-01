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

  getOrders(): Observable<any> { // 修改返回类型为具体的订单类型
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

}
