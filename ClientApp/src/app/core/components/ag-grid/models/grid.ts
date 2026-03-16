import { Layout } from "./layout";
import { Row } from "./row";

export interface Grid {
    layout: Layout;
    rows: Row<unknown>[];
}