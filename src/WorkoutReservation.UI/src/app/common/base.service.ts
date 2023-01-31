import {HttpClient, HttpHeaders, HttpParams, HttpResponse} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {map, Observable} from "rxjs";
import {environment} from "src/environments/environment";

export const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  body: {},
  observe: 'response' as 'body',
  params: {},
  withCredentials: true
};

@Injectable({providedIn: 'root'})
export abstract class BaseService {
  protected httpOptions: { headers: HttpHeaders, body: {}, observe: string, params: {} | HttpParams };

  constructor(protected http: HttpClient) {
    this.httpOptions = httpOptions;
  }

  protected get<T>(url: string, params?: any, options?: any): Observable<T> {
    return this.request<T>('GET', url, null, params, options)
      .pipe(map((response: HttpResponse<T>) => response.body as T));
  }

  protected post<T>(url: string, data?: any, params?: any, options?: any): Observable<T> {
    return this.request<T>('POST', url, data, params, options)
      .pipe(map((response: HttpResponse<T>) => response.body as T));
  }

  protected put<T>(url: string, data?: any, params?: any, options?: any): Observable<T> {
    return this.request<T>('PUT', url, data, params, options)
      .pipe(map((response: HttpResponse<T>) => response.body as T));
  }

  protected patch<T>(url: string, data?: any, params?: any, options?: any): Observable<T> {
    return this.request<T>('PATCH', url, data, params, options)
      .pipe(map((response: HttpResponse<T>) => response.body as T));
  }

  protected delete<T>(url: string, data?: any, params?: any, options?: any): Observable<T> {
    return this.request<T>('DELETE', url, data, params, options)
      .pipe(map((response: HttpResponse<T>) => response.body as T));
  }

  private request<T>(method: string, url: string, data?: any, params?: any, options?: {})
    : Observable<HttpResponse<T>> {
      const requestOptions = Object.assign({}, httpOptions);
      Object.assign(requestOptions, options);
      requestOptions.body = data;
      requestOptions.params = params;
      return this.http.request<HttpResponse<T>>(method, `${environment.apiUrl}${url}`, requestOptions);
    }
}


