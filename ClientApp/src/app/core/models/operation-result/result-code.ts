export enum ResultCode {
    Success = 1,
    Error = 2,
    Warning = 3,
}

export function isSuccess(code: ResultCode): boolean {
    return code === ResultCode.Success;
}

export function isError(code: ResultCode): boolean {
    return code === ResultCode.Error;
}