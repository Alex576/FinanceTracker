import { InjectionToken } from "@angular/core";
import { FullScreenPanelData } from "./full-screen-panel-data";

export const FULL_SCREEN_PANEL_DATA = new InjectionToken<FullScreenPanelData>('FULL_SCREEN_PANEL_DATA');
export const FULL_SCREEN_PANEL_HEADER = new InjectionToken<string>('FULL_SCREEN_PANEL_HEADER');