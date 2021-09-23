"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.responseFailedInterceptor = exports.responseSuccessInterceptor = exports.handleCommonResponse = exports.requestInterceptor = void 0;
function requestInterceptor(req) {
    var btn = document.getElementById('form-btn');
    var loading = document.getElementById('loading');
    var message = document.getElementById('message');
    var errorMessage = document.getElementById('errorMessage');
    for (var key in req.data) {
        var error = document.getElementById(key.toUpperCase() + "ERROR");
        if (error) {
            error.innerHTML = "";
        }
    }
    if (errorMessage) {
        errorMessage.innerHTML = '';
    }
    if (message) {
        message.innerHTML = '';
    }
    if (loading && btn) {
        btn.classList.add('hidden');
        loading.classList.remove('hidden');
        loading.classList.add('flex');
    }
    return req;
}
exports.requestInterceptor = requestInterceptor;
function handleCommonResponse() {
    var btn = document.getElementById('form-btn');
    var loading = document.getElementById('loading');
    if (loading && btn) {
        btn.classList.remove('hidden');
        btn.classList.add('block');
        loading.classList.add('hidden');
        loading.classList.remove('flex');
    }
}
exports.handleCommonResponse = handleCommonResponse;
function responseSuccessInterceptor(response) {
    var _a, _b, _c, _d;
    handleCommonResponse();
    if ((_b = (_a = response === null || response === void 0 ? void 0 : response.data) === null || _a === void 0 ? void 0 : _a.details) === null || _b === void 0 ? void 0 : _b.message) {
        var message = document.getElementById('MESSAGEERROR');
        if (message) {
            message.innerHTML = (_d = (_c = response === null || response === void 0 ? void 0 : response.data) === null || _c === void 0 ? void 0 : _c.details) === null || _d === void 0 ? void 0 : _d.message;
        }
    }
    return response;
}
exports.responseSuccessInterceptor = responseSuccessInterceptor;
function responseFailedInterceptor(error) {
    var _a, _b;
    handleCommonResponse();
    if ((_b = (_a = error.response) === null || _a === void 0 ? void 0 : _a.data) === null || _b === void 0 ? void 0 : _b.details) {
        var details = error.response.data.details;
        for (var key in details) {
            var label = document.getElementById(key.toUpperCase() + "LABEL");
            var error_1 = document.getElementById(key.toUpperCase() + "ERROR");
            if (label && error_1) {
                error_1.innerHTML = label.innerHTML + " " + details[key];
            }
            else if (error_1 && (key === 'errorMessage' || key === 'message')) {
                error_1.innerHTML = "" + details[key];
            }
        }
    }
    return Promise.reject(error.response);
}
exports.responseFailedInterceptor = responseFailedInterceptor;
