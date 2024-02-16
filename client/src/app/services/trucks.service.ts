import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Truck } from './api';


@Injectable({
  providedIn: 'root'
})
export class TrucksService {
  private apiUrl = 'http://localhost:5000/api/trucks'; // Adjust accordingly

  constructor(private http: HttpClient) { }

  getTrucks(): Observable<Truck[]> {
    return this.http.get<Truck[]>(this.apiUrl);
  }
}
