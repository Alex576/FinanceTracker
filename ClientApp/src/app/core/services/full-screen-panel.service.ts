import { Overlay, OverlayConfig, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { inject, Injectable, Injector, Provider } from '@angular/core';
import { FullScreenPanelConfig } from '../models/full-screen-panel/full-screen-panel-config';
import { FullScreenPanelData } from '../models/full-screen-panel/full-screen-panel-data';
import { FULL_SCREEN_PANEL_DATA, FULL_SCREEN_PANEL_HEADER } from '../models/full-screen-panel/full-screen-panel-token';

@Injectable({
  providedIn: 'root'
})
export class FullScreenPanelService {
  private readonly overlay = inject(Overlay);
  private readonly injector = inject(Injector);

  private overlayRef: OverlayRef;

  constructor() { }


  openFullScreenPanel<T, D>(data: FullScreenPanelConfig<T, D>, providers: Provider[] = []): void {
    this.close();

    const config: OverlayConfig = {
      positionStrategy: this.overlay.position().global().centerHorizontally().centerVertically(),
      hasBackdrop: true,
      usePopover: false,
      panelClass: 'full-screen-panel',
      scrollStrategy: this.overlay.scrollStrategies.block(),
    };
    this.overlayRef = this.overlay.create(config);

    const panelData: FullScreenPanelData = {
      tileCode: data.tileCode,
      data: data.data,
    };
    const panelInjector = Injector.create({
      providers: [
        { provide: FULL_SCREEN_PANEL_DATA, useValue: panelData },
        { provide: FULL_SCREEN_PANEL_HEADER, useValue: data.header },
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
