import { InjectionToken } from "@angular/core";
import { SidePanelData } from "./side-panel-data";

export const SIDE_PANEL_DATA = new InjectionToken<SidePanelData>('SIDE_PANEL_DATA');
export const SIDE_PANEL_HEADER = new InjectionToken<string>('SIDE_PANEL_HEADER');