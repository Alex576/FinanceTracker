import { ResultCode } from "./result-code";

export interface OperationResult<T> {
    result: T;
    code: ResultCode;
    description?: string;
}