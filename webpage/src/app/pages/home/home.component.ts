import { Component, inject, OnInit, signal } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { FormsModule } from '@angular/forms';
import { JsonViewerComponent } from '../../components/json-viewer/json-viewer.component';

@Component({
  selector: 'app-home',
  imports: [FormsModule, JsonViewerComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  apiUrl: string;
  requestUrl = 'senior/contests/2025';
  response = signal<object | null>(null);
  hasError = signal<boolean>(false);

  private api = inject(ApiService);

  async ngOnInit(): Promise<void> {
    this.apiUrl = this.api.getApiUrl();
    await this.sendRequest();
  }

  copyToClipboard() {
    navigator.clipboard.writeText(`${this.apiUrl}${this.requestUrl}`);
  }

  async sendRequest(): Promise<void> {
    try {
      this.response.set(await this.api.get(this.requestUrl));
      this.hasError.set(false);
    } catch (error) {
      this.hasError.set(true);
    }
  }
}
