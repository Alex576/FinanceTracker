import { UserSettingsModel } from "./user-settings-model";

export interface SaveUserSettingsModel extends UserSettingsModel {
    value: unknown;
}