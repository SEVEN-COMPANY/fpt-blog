//@ts-nocheck
import { AxiosStatic } from 'axios';

const http = axios as AxiosStatic;
http.defaults.withCredentials = true;

export { http };
