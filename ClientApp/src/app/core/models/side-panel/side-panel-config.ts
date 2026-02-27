import { ComponentType } from "@angular/cdk/overlay";
import { SidePanelData } from "./side-panel-data";
import { SidePanelType } from "./side-panel-type";

export interface SidePanelConfig<T> {
    type: SidePanelType,
    componentType: ComponentType<T>;
    data: SidePanelData;
    header: string;
}
