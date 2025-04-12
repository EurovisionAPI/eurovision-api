import { NgClass, NgTemplateOutlet } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-json-viewer',
  imports: [NgClass, NgTemplateOutlet, FormsModule],
  templateUrl: './json-viewer.component.html',
  styleUrl: './json-viewer.component.css',
})
export class JsonViewerComponent implements OnChanges {
  @Input() json: any;
  @Input() maxSizeStartExpanded: number = 3;

  jsonRoot: JsonNode;
  jsonRaw: string;
  totalLines: number;
  totalBytes: number;
  showRaw: boolean = false;

  ngOnChanges(changes: SimpleChanges): void {
    if (this.json) {
      this.jsonRoot = this.createNode('root', this.json);
      this.jsonRaw = JSON.stringify(this.json, null, 2);
      this.totalLines = this.jsonRaw.split('\n').length;
      this.totalBytes = new TextEncoder().encode(this.jsonRaw).length / 1000;
    } else {
      this.jsonRoot = this.jsonRaw = this.totalLines = null;
    }
  }

  getChildren(node: JsonNode): JsonNode[] {
    if (!node.children) {
      node.children = this.getNodes(node.value);
    }

    return node.children;
  }

  private createNode(key: string, value: any): JsonNode {
    const type = this.getType(value);
    const isExpandable = type == JsonType.Object || type == JsonType.Array;
    const size = this.getSize(type, value);
    const info = this.getInfo(type, size);

    switch (type) {
      case JsonType.Null:
        value = 'null';
        break;
      case JsonType.String:
        value = '"' + value + '"';
        break;
    }

    return {
      type,
      key,
      value,
      isExpandable,
      expanded: size && size <= this.maxSizeStartExpanded,
      info,
      children: null,
    };
  }

  private getNodes(obj: any): JsonNode[] {
    if (obj === null || obj === undefined) return [];

    const nodes: JsonNode[] = [];

    for (const key of Object.keys(obj)) {
      const value = obj[key];

      nodes.push(this.createNode(key, value));
    }

    return nodes;
  }

  private getType(value: any): JsonType {
    if (value === null) {
      return JsonType.Null;
    } else if (Array.isArray(value)) {
      return JsonType.Array;
    } else {
      switch (typeof value) {
        case 'boolean':
          return JsonType.Boolean;
        case 'number':
        case 'bigint':
          return JsonType.Number;
        case 'string':
          return JsonType.String;
        default:
          return JsonType.Object;
      }
    }
  }

  private getSize(type: JsonType, value: any): number {
    let size = null;

    if (type == JsonType.Object) {
      size = Object.keys(value).length;
    } else if (type == JsonType.Array) {
      size = value.length;
    }

    return size;
  }

  private getInfo(type: JsonType, size: number) {
    let info = null;

    if (type == JsonType.Object) {
      info = `{} ${size} key`;
    } else if (type == JsonType.Array) {
      info = `[] ${size} item`;
    }

    if (info && size > 1) {
      info += 's';
    }

    return info;
  }

  toggleNode(node: JsonNode): void {
    node.expanded = !node.expanded;
  }
}

enum JsonType {
  Null = 'null',
  Boolean = 'boolean',
  Number = 'number',
  String = 'string',
  Object = 'object',
  Array = 'array',
}

interface JsonNode {
  type: JsonType;
  key: string;
  value: any;
  //level: number;
  isExpandable: boolean;
  expanded: boolean;
  info: string;
  children: JsonNode[];
  //path: string[];
}

/*
interface JsonNode {
  key: string;
  value: any;
  level: number;
  type: Type;

  isExpandable: boolean;
  expanded: boolean;
  path: string[];
}*/
