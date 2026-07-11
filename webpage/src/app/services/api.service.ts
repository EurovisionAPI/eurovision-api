import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly apiUrl: string = environment.apiUrl;
  private readonly httpClient = inject(HttpClient);

  getApiUrl(): string {
    return this.apiUrl;
  }

  get(url: string): Promise<any> {
    return lastValueFrom(this.httpClient.get(this.apiUrl + url));
  }
}
