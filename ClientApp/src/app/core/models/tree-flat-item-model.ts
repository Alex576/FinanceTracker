export interface TreeFlatItem<T extends FlatTreeEntity> {
    data: T;
    hasChild: boolean;
    expanded?: boolean;
    level: number;
    visible?: boolean;
    // children?: TreeFlatItem<T>[];
    // parent?: TreeFlatItem<T>;
}

export interface FlatTreeEntity {
    id: number;
    parentId?: number;
}