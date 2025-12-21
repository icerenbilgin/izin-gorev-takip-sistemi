import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'http://localhost:5121/api/User';

  constructor(private http: HttpClient) {}

  login(payload: { email: string; password: string }) {
    return this.http.post(`${this.apiUrl}/login`, payload);
  }
}
