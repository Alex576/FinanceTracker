import { Overlay, OverlayConfig, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { inject, Injectable, Injector } from '@angular/core';
import { SidePanelConfig } from '../models/side-panel/side-panel-config';
import { SIDE_PANEL_DATA, SIDE_PANEL_HEADER } from '../models/side-panel/side-panel-data-token';

@Injectable({
  providedIn: 'root'
})
export class SidePanelService {
  private readonly overlay = inject(Overlay);
  private readonly injector = inject(Injector);

  private overlayRef: OverlayRef;

  constructor() { }


  openSidePanel<T>(data: SidePanelConfig<T>): void {
    this.close();

    const config: OverlayConfig = {
      positionStrategy: this.overlay.position().global().right().top(),
      hasBackdrop: false,
      panelClass: 'side-panel',
      scrollStrategy: this.overlay.scrollStrategies.block(),
    };
    this.overlayRef = this.overlay.create(config);

    const panelInjector = Injector.create({
      providers: [
        { provide: SIDE_PANEL_DATA, useValue: data.data },
        { provide: SIDE_PANEL_HEADER, useValue: data.header }
      ],
      parent: this.injector,
    });
    const componentPortal = new ComponentPortal(data.componentType, null, panelInjector);
    this.overlayRef.attach(componentPortal);
  }

  close(): void {
    if (this.overlayRef != null) {
      this.overlayRef.dispose();
      this.overlayRef = null;
    }
  }
}
