//@ts-nocheck
import { AxiosStatic } from 'axios';
import { requestInterceptor, responseFailedInterceptor, responseSuccessInterceptor } from './interceptor';

const http = axios as AxiosStatic;
http.defaults.withCredentials = true;

http.interceptors.request.use(requestInterceptor);
http.interceptors.response.use(responseSuccessInterceptor, responseFailedInterceptor);

export { http };
