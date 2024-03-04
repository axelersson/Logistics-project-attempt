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

  newOrder(userId: string, order: any): Observable<any> {
    const url = `${this.baseUrl}/Orders/NewOrder/${userId}`;
    return this.http.post<any>(url, order, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(
      catchError(error => throwError(() => new Error(`Create order failed: ${error.message}`)))
    );
  }
  
  // 在 order.service.ts 中
 cancelOrder(orderId: string): Observable<void> {
  let url = `${this.baseUrl}/Orders/Cancel/${orderId}`; // 注意这里使用反引号和变量替换
  return this.http.put<void>(url, {}).pipe(
    catchError(error => {
      throw new Error(`Error in cancelling order: ${error.message}`);
    })
  );
 }

 getOrderById(orderId: string): Observable<any> {
  let url = this.baseUrl + `/Orders/${orderId}`; // 确保你的URL是正确的
  return this.http.get<any>(url, {
    headers: new HttpHeaders({
      'Accept': 'application/json',
    }),
  }).pipe(
    map(response => response), // 如果后端直接返回了订单对象，这里不需要改变
    catchError(error => throwError(() => new Error(`Failed to get order with ID ${orderId}: ${error.message}`)))
  );
}

 updateOrder(orderId: string, orderData: any): Observable<void> {
  const url = `${this.baseUrl}/Orders/${orderId}`;
  return this.http.put<void>(url, orderData, {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  }).pipe(
    catchError(error => {
      throw new Error(`Error updating order: ${error.message}`);
    })
  );
}

}
