import { ResultCode } from "./result-code";

export interface OperationResult {
    code: ResultCode;
    description?: string;
}

export interface OperationResultData<T> extends OperationResult {
    result: T;
}