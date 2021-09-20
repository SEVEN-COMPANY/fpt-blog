import { routers } from './package/axios/routes';
import { http } from './package/axios';
import { ServerResponse } from './package/interface/serverResponse';

interface User {
    address: string;
    avatarUrl: string;
    createDate: string;
    name: string;

    phone: string;
    userId: string;
    username: string;
}

const getCurrentUser = () => {
    http.get<ServerResponse<User>>(routers.getUser).then((res) => {
        const user = document.getElementById('user');
        const auth = document.getElementById('auth');
        const userAvatar = document.getElementById('user-avatar') as HTMLImageElement;
        if (user && userAvatar) {
            user.classList.remove('hidden');
            user.classList.add('flex');
            userAvatar.src = res.data.data.avatarUrl;
        }

        if (auth) {
            auth.classList.add('hidden');
            auth.classList.remove('md:flex');
        }
    });
};

getCurrentUser();
