import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

interface UpdateUserDto {
    name: string;
    email: string;
    address: string;
    phone: string;
}

const updateUserForm = document.getElementById('updateUserForm');
updateUserForm?.addEventListener('submit', function (event: Event) {
    event.preventDefault();

    const name = document.getElementById('name') as HTMLInputElement;
    const email = document.getElementById('email') as HTMLInputElement;
    const address = document.getElementById('address') as HTMLInputElement;
    const phone = document.getElementById('phone') as HTMLInputElement;

    if (name !== null && email !== null && address !== null && phone !== null) {
        const input: UpdateUserDto = {
            name: name.value,
            email: email.value,
            address: address.value,
            phone: phone.value,
        };

        http.put<ServerResponse<null>>(routers.user.update, input);
    }
});
