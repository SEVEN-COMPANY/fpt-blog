import { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios';
import { ServerResponse } from '../interface/serverResponse';

export function requestInterceptor(req: AxiosRequestConfig) {
    const btn = document.getElementById('form-btn');
    const loading = document.getElementById('loading');
    const message = document.getElementById('MESSAGEERROR');
    const errorMessage = document.getElementById('ERRORMESSAGEERROR');

    for (const key in req.data) {
        const error = document.getElementById(`${key.toUpperCase()}ERROR`);
        if (error) {
            error.innerHTML = ``;
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

export function handleCommonResponse() {
    const btn = document.getElementById('form-btn');
    const loading = document.getElementById('loading');

    if (loading && btn) {
        btn.classList.remove('hidden');
        btn.classList.add('block');
        loading.classList.add('hidden');
        loading.classList.remove('flex');
    }
}

export function responseSuccessInterceptor(response: AxiosResponse<any>) {
    handleCommonResponse();

    if (response?.data?.details?.message) {
        const message = document.getElementById('MESSAGEERROR');
        if (message) {
            message.innerHTML = response?.data?.details?.message;
        }
    }
    return response;
}

export function responseFailedInterceptor(error: AxiosError<ServerResponse<null>>) {
    handleCommonResponse();

    if (error.response?.data?.details) {
        const details = error.response.data.details;
        for (const key in details) {
            const error = document.getElementById(`${key.toUpperCase()}ERROR`);

            if (error) {
                error.innerHTML = `${error.getAttribute('data-label')} ${details[key]}`;
            }

            if (error && (key === 'errorMessage' || key === 'message')) {
                error.innerHTML = `${details[key]}`;
            }
        }
    }

    return Promise.reject(error.response);
}
