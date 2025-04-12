import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { JsonViewerComponent } from '../../components/json-viewer/json-viewer.component';
import { SafeHtml } from '@angular/platform-browser';
import { DocumentationService } from '../../services/documentation.service';
import { DocItemType, Documentation } from '../../models/documentation';
import { NgTemplateOutlet } from '@angular/common';

@Component({
  selector: 'app-documentation',
  imports: [JsonViewerComponent, NgTemplateOutlet],
  templateUrl: './documentation.component.html',
  styleUrl: './documentation.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class DocumentationComponent implements OnInit {
  DocItemType = DocItemType;

  documentation: Documentation;

  constructor(private service: DocumentationService) {}

  async ngOnInit(): Promise<void> {
    this.documentation = await this.service.getDocumentation();

    /*
    // Reemplazar bloques de metadatos con componentes
    setTimeout(() => {
      const container = this.markdownContainer.nativeElement;
      const metaBlocks = container.querySelectorAll('div[metadata]');

      metaBlocks.forEach((block: HTMLElement) => {
        const meta = block.getAttribute('metadata');
        console.log(meta)
        const viewRef = this.viewContainerRef.createEmbeddedView(this.endpointViewer, {
          data: meta
        });

        // Reemplazamos el bloque con el contenido del ng-template
        block.replaceWith(viewRef.rootNodes[0]);
      });
    }, 0);*/
  }
}
