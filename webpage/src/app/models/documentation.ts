import { SafeHtml } from "@angular/platform-browser";

export class Documentation {
  content: DocItem[];

  constructor() {
    this.content = [];
  }

  addHtml(html: SafeHtml) {
    this.content.push({ type: DocItemType.HTML, value: html });
  }

  addMetadata(data: Object) {
    this.content.push({ type: DocItemType.METADATA, value: data });
  }
}

export interface DocItem {
  type: DocItemType,
  value: any
}

export enum DocItemType {
  HTML,
  METADATA
}