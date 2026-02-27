import { ToolCode } from "../tool-code";
import { UserSetting } from "./user-setting";

export interface LastSessionSetting extends UserSetting {
    lastOpenedTool: ToolCode;
}