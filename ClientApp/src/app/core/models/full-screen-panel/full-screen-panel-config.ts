import { ComponentType } from "@angular/cdk/overlay";
import { TileCode } from "../tile-code";
import { FullScreenPanelType } from "./full-screen-panel-type";

export interface FullScreenPanelConfig<T, D = unknown> {
    type: FullScreenPanelType,
    componentType: ComponentType<T>;
    tileCode: TileCode,
    data: D;
    header: string;
}