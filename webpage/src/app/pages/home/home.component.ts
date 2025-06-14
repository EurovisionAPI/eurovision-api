import { Component, OnInit } from '@angular/core';
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
  response: object;
  hasError: boolean;

  constructor(private api: ApiService) {}

  async ngOnInit(): Promise<void> {
    this.apiUrl = this.api.getApiUrl();
    await this.sendRequest();
  }

  copyToClipboard() {
    navigator.clipboard.writeText(`${this.apiUrl}${this.requestUrl}`);
  }

  async sendRequest(): Promise<void> {
    try {
      this.response = await this.api.get(this.requestUrl);
      this.hasError = false;
    } catch (error) {
      this.hasError = true;
    }
  }
}
