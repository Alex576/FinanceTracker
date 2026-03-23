import { TileCode } from "../tile-code";

export interface SidePanelData<D = unknown> {
    tileCode: TileCode;
    data: D;
}