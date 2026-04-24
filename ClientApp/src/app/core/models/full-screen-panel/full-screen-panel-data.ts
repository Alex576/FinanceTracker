import { TileCode } from "../tile-code";

export interface FullScreenPanelData<D = unknown> {
    tileCode: TileCode;
    data: D;
}