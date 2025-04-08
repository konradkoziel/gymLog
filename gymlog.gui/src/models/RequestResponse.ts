export default interface RequestResponse<T> {
    data: T | null;
    success: boolean;
    message: string;
}
