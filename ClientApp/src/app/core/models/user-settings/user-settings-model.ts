import { TileCode } from "../tile-code";
import { ToolCode } from "../tool-code";
import { UserSettingCode } from "../user-setting-code";

export interface UserSettingsModel {
    settingCode: UserSettingCode;
    toolCode?: ToolCode;
    tileCode?: TileCode;
}