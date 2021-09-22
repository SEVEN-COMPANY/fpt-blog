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
        const name = document.getElementById('user-name');

        const username = document.getElementById('user-username');
        const userAvatar = document.getElementById('user-avatar') as HTMLImageElement;

        if (name !== null && userAvatar !== null && username !== null) {
            name.innerHTML = res.data.data.name;
            username.innerHTML = res.data.data.username;
            userAvatar.src = res.data.data.avatarUrl;
        }

        // if (createDate) {
        //     createDate.classList.add('hidden');
        //     createDate.classList.remove('md:flex');
        // }
    });
};

getCurrentUser();
