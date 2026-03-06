import { FormControl } from "./controls/form-control";
import { ToolCode } from "./tool-code";

export interface GetGridLayoutModel {
    toolCode: ToolCode;
    filters: FormControl[];
}