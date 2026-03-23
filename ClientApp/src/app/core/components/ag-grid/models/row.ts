import { RowAction } from "./row-action";
import { RowTag } from "./row-tag";

export interface Row<TData extends RowTag = RowTag> {
    data: unknown[];
    actions: RowAction[];
    tag: TData;
}