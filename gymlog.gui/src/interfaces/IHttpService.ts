import RequestResponse from "../models/RequestResponse";

export default interface IHttpService {
    get<T>(endpoint: string): Promise<RequestResponse<T>>;
    post<T>(endpoint: string, body: unknown): Promise<RequestResponse<T>>;
    delete<T>(endpoint: string, body: unknown): Promise<RequestResponse<T>>;
    patch<T>(endpoint: string, body: unknown): Promise<RequestResponse<T>>;
}