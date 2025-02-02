import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { FormsModule } from '@angular/forms';
import { JsonViewerComponent } from '../../components/json-viewer/json-viewer.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, JsonViewerComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  apiUrl: string;
  requestUrl = 'contests/2024';
  response: object;

  constructor(private api: ApiService) { }

  async ngOnInit(): Promise<void> {
    this.apiUrl = this.api.getApiUrl();
    await this.sendRequest();
  }

  copyToClipboard() {
    navigator.clipboard.writeText(`${this.apiUrl}${this.requestUrl}`);
  }

  async sendRequest(): Promise<void> {
    this.response = await this.api.get(this.requestUrl);
  }
}
