import { FormControlValue } from "./controls/form-control-value";
import { ToolCode } from "./tool-code";

export interface GetGridLayoutModel {
    toolCode: ToolCode;
    filters: FormControlValue[];
}