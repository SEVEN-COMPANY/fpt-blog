"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.http = void 0;
var interceptor_1 = require("./interceptor");
var http = axios;
exports.http = http;
http.defaults.withCredentials = true;
http.interceptors.request.use(interceptor_1.requestInterceptor);
http.interceptors.response.use(interceptor_1.responseSuccessInterceptor, interceptor_1.responseFailedInterceptor);
