import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler,
  ): Observable<HttpEvent<any>> {
    // Retrieve your JWT token from where it's stored (e.g., local storage)
    const jwtToken = localStorage.getItem('token');

    // Check if the token exists
    if (jwtToken) {
      // Clone the request to add the new header.
      const clonedRequest = request.clone({
        setHeaders: {
          Authorization: `Bearer ${jwtToken}`,
        },
      });

      // Pass the cloned request instead of the original request to the next handler.
      return next.handle(clonedRequest);
    }

    // If there's no token, just forward the original request
    return next.handle(request);
  }
}
