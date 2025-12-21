import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly baseUrl = 'http://localhost:5121/api';

  constructor(private http: HttpClient) {}

  get<T>(endpoint: string) {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}`);
  }

  getById<T>(endpoint: string, id: number) {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`);
  }

  post<T>(endpoint: string, body: any) {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body);
  }

  delete<T>(endpoint: string, id: number) {
    return this.http.delete<T>(`${this.baseUrl}/${endpoint}/${id}`);
  }
}