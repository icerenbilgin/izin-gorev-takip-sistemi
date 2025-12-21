import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiEndpoints } from '../api-endpoints';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class HttpEntityService {

  private readonly baseUrl = environment.API_BASE_URL.replace(/\/$/, '');

  constructor(private http: HttpClient) {}

  private buildUrl(url: string): string {
    return `${this.baseUrl}/${url.replace(/^\//, '')}`;
  }

  get<T>(url: string): Observable<T> {
    return this.http.get<T>(this.buildUrl(url));
  }

  getById<T>(url: string, id: number): Observable<T> {
    return this.http.get<T>(`${this.buildUrl(url)}/${id}`);
  }

  post<T>(url: string, body: any): Observable<T> {
    return this.http.post<T>(this.buildUrl(url), body);
  }

  delete<T>(url: string, id: number): Observable<T> {
    return this.http.delete<T>(`${this.buildUrl(url)}/${id}`);
  }
}
