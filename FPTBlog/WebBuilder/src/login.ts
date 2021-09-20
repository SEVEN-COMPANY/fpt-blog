import { http } from './package/axios';

interface LoginUserDto {
    Username: string;
    Password: string;
}

interface Hello {
    data: {
        details: LoginUserDto;
    };
}

const loginForm = document.getElementById('loginForm');
loginForm?.addEventListener('submit', function (event: Event) {
    event.preventDefault();

    const username = document.getElementById('username') as HTMLInputElement;
    const password = document.getElementById('password') as HTMLInputElement;

    if (username && password) {
        http.post('/api/auth/login', { username: username.value, password: password.value })
            .then((res) => console.log(res))
            .catch(({ response }) => {
                const res = response as Hello;
                console.log(res.data.details.Password);
                const usernameError = document.getElementById('usernameError') as HTMLInputElement;
                usernameError.innerHTML = 'Username ' + res.data.details.Username;
            });
    }
});
