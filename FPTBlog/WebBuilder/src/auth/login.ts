import { http } from '../package/axios';
import { routerLinks, routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';
interface LoginUserDto {
    username: string;
    password: string;
}

const loginForm = document.getElementById('loginForm');
loginForm?.addEventListener('submit', function (event: Event) {
    event.preventDefault();

    const username = document.getElementById('username') as HTMLInputElement;
    const password = document.getElementById('password') as HTMLInputElement;

    if (username && password) {
        const input: LoginUserDto = {
            username: username.value,
            password: password.value,
        };

        http.post<ServerResponse<null>>(routers.loginUser, input).then(() => window.location.assign(routerLinks.home));
    }
});
