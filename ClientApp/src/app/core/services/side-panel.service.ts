import { Overlay, OverlayConfig, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { inject, Injectable, Injector, Provider } from '@angular/core';
import { SidePanelConfig } from '../models/side-panel/side-panel-config';
import { SidePanelData } from '../models/side-panel/side-panel-data';
import { SIDE_PANEL_DATA, SIDE_PANEL_HEADER } from '../models/side-panel/side-panel-data-token';

@Injectable({
  providedIn: 'root'
})
export class SidePanelService {
  private readonly overlay = inject(Overlay);
  private readonly injector = inject(Injector);

  private overlayRef: OverlayRef;

  constructor() { }


  openSidePanel<T, D>(data: SidePanelConfig<T, D>, providers: Provider[] = []): void {
    this.close();

    const config: OverlayConfig = {
      positionStrategy: this.overlay.position().global().right().top(),
      hasBackdrop: false,
      usePopover: false,
      panelClass: 'side-panel',
      scrollStrategy: this.overlay.scrollStrategies.block(),
    };
    this.overlayRef = this.overlay.create(config);

    const panelData: SidePanelData = {
      tileCode: data.tileCode,
      data: data.data,
    };
    const panelInjector = Injector.create({
      providers: [
        { provide: SIDE_PANEL_DATA, useValue: panelData },
        { provide: SIDE_PANEL_HEADER, useValue: data.header },
        ...providers,
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
