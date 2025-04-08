import IHttpService from "../interfaces/IHttpService";
import RequestResponse from "../models/RequestResponse";
import HttpMethod from "../utils/enums/HttpMethod";

export class HttpService implements IHttpService {
  private readonly apiUrl: string;

  constructor(
    apiUrl: string = `${import.meta.env.VITE_REACT_APP_API_URL}/api/`
  ) {
    this.apiUrl = apiUrl;
  }

  public get<T>(endpoint: string): Promise<RequestResponse<T>> {
    return this.request(endpoint, HttpMethod.GET);
  }

  public post<T>(endpoint: string, body: unknown): Promise<RequestResponse<T>> {
    return this.request(endpoint, HttpMethod.POST, body);
  }

  public delete<T>(
    endpoint: string,
    body: unknown
  ): Promise<RequestResponse<T>> {
    return this.request(endpoint, HttpMethod.DELETE, body);
  }

  public patch<T>(
    endpoint: string,
    body: unknown
  ): Promise<RequestResponse<T>> {
    return this.request(endpoint, HttpMethod.PATCH, body);
  }

  private async request<T>(
    endpoint: string,
    method: HttpMethod,
    body?: unknown
  ): Promise<RequestResponse<T>> {
    const response = await fetch(`${this.apiUrl}${endpoint}`, {
      method,
      headers: { "Content-Type": "application/json" },
      body: body ? JSON.stringify(body) : undefined,
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    return response.json();
  }
}

const httpService: IHttpService = new HttpService();
export default httpService;
