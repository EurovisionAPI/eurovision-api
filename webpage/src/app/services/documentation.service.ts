import { Injectable } from '@angular/core';
import { Documentation } from '../models/documentation';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { marked } from 'marked';
import { lastValueFrom } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DocumentationService {
  readonly FILE_URL = 'docs/documentation.md';

  private documentation: Documentation;
  private apiUrl: string = environment.apiUrl;

  constructor(
    private httpClient: HttpClient,
    private sanitizer: DomSanitizer,
  ) {
    marked.setOptions({
      gfm: true, // Soporte para GitHub Flavored Markdown
      breaks: true, // Saltos de línea automáticos
    });
  }

  async getDocumentation(): Promise<Documentation> {
    if (!this.documentation) {
      const documentation = new Documentation();
      const markdown = await lastValueFrom(
        this.httpClient.get(this.FILE_URL, { responseType: 'text' }),
      );

      // Detectar metadatos
      const metaDataRegex = /@{.*?}@/gs;
      const matches = markdown.matchAll(metaDataRegex);
      let startHtmlIndex = 0;

      for (const match of matches) {
        this.addHtml(startHtmlIndex, match.index, markdown, documentation);
        this.addMetadata(match, documentation);

        startHtmlIndex = match.index + match[0].length;
      }

      // Add the rest as HTML
      this.addHtml(startHtmlIndex, markdown.length, markdown, documentation);
      this.documentation = documentation;
    }

    return this.documentation;
  }

  private addHtml(
    startIndex: number,
    end: number,
    markdown: string,
    documentation: Documentation,
  ) {
    const subMarkdown = markdown.substring(startIndex, end);

    if (subMarkdown.length > 0) {
      const unsafeHtml = marked.parse(subMarkdown) as string;
      const safeHtml = this.sanitizer.bypassSecurityTrustHtml(unsafeHtml);
      documentation.addHtml(safeHtml);
    }
  }

  private addMetadata(match: RegExpExecArray, documentation: Documentation) {
    const rawMatch = match[0];
    const dataJson = rawMatch.substring(1, rawMatch.length - 1); // Remove the @ from the beginning and the end
    const data = JSON.parse(dataJson);

    if (data.endpoint) {
      data.endpoint = `${this.apiUrl}${data.endpoint}`;
    }

    documentation.addMetadata(data);
  }
}
