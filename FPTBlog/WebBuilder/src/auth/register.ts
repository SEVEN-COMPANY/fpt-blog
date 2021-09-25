import { http } from '../package/axios';
import { routerLinks, routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

interface RegisterUserDto {
    username: string;
    password: string;
    confirmPassword: string;
    name: string;
}

const registerForm = document.getElementById('registerForm');
registerForm?.addEventListener('submit', function (event: Event) {
    event.preventDefault();

    const username = document.getElementById('username') as HTMLInputElement;
    const password = document.getElementById('password') as HTMLInputElement;
    const name = document.getElementById('name') as HTMLInputElement;
    const confirmPassword = document.getElementById('confirmPassword') as HTMLInputElement;

    if (username && password && name && confirmPassword) {
        const input: RegisterUserDto = {
            username: username.value,
            password: password.value,
            name: name.value,
            confirmPassword: confirmPassword.value,
        };

        http.post<ServerResponse<null>>(routers.registerUser, input).then(() => window.location.assign(routerLinks.loginForm));
    }
});
