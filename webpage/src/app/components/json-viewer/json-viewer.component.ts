import { NgClass, NgTemplateOutlet } from '@angular/common';
import { Component, computed, input, signal, WritableSignal } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-json-viewer',
  imports: [NgClass, NgTemplateOutlet, FormsModule],
  templateUrl: './json-viewer.component.html',
  styleUrl: './json-viewer.component.css',
})
export class JsonViewerComponent {
  readonly json = input.required<any>();
  readonly maxSizeStartExpanded = input<number>(3);

  readonly showRaw = signal(false);
  readonly parsedJson = computed<ParsedJson>(() => {
    const json = this.json();
    if (json == null) {
      return { root: null, raw: null, totalLines: 0, totalBytes: 0 };
    }

    const root = this.createNode('root', json, true);
    const raw = JSON.stringify(json, null, 2);
    const totalLines = raw.split('\n').length;
    const totalBytes = new TextEncoder().encode(raw).length / 1000;

    return { root, raw, totalLines, totalBytes };
  });

  private createNode(key: string, value: any, isRoot: boolean = false): JsonNode {
    const type = this.getType(value);
    const isExpandable = type == JsonType.Object || type == JsonType.Array;
    const size = this.getSize(type, value);
    const info = this.getInfo(type, size);
    const expanded = 0 <= size && size <= this.maxSizeStartExpanded();
    const children = isRoot || (isExpandable && expanded) ? this.getNodes(value) : null;

    switch (type) {
      case JsonType.Null:
        value = 'null';
        break;
      case JsonType.String:
        value = `"${value}"`;
        break;
    }

    return {
      type,
      key,
      value,
      isExpandable,
      expanded: signal(expanded),
      info,
      children: signal(children),
    };
  }

  private getNodes(obj: any): JsonNode[] {
    const nodes: JsonNode[] = [];

    if (obj != null && obj !== undefined) {
      for (const key of Object.keys(obj)) {
        const value = obj[key];

        nodes.push(this.createNode(key, value));
      }
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

  private getInfo(type: JsonType, size: number): string | null {
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
    const willExpand = !node.expanded();

    if (willExpand && !node.children()) {
      node.children.set(this.getNodes(node.value));
    }

    node.expanded.set(willExpand);
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

interface ParsedJson {
  root: JsonNode | null;
  raw: string | null;
  totalLines: number;
  totalBytes: number;
}

interface JsonNode {
  type: JsonType;
  key: string;
  value: any;
  isExpandable: boolean;
  expanded: WritableSignal<boolean>;
  info: string | null;
  children: WritableSignal<JsonNode[] | null>;
}