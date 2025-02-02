import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  getApiUrl(): string {
    return this.apiUrl;
  }

  get(url: string): Promise<any> {
    return lastValueFrom(this.httpClient.get(this.apiUrl + url));
  }
}
