import { ComponentType } from "@angular/cdk/overlay";
import { TileCode } from "../tile-code";
import { SidePanelType } from "./side-panel-type";

export interface SidePanelConfig<T, D = unknown> {
    type: SidePanelType,
    componentType: ComponentType<T>;
    tileCode: TileCode,
    data: D;
    header: string;
}
