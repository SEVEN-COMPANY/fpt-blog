import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

interface ChangePasswordDto {
    oldPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}

const changeUserPassword = document.getElementById('changeUserPasswordForm');
changeUserPassword?.addEventListener('submit', function (event: Event) {
    event.preventDefault();

    const oldPassword = document.getElementById('oldPassword') as HTMLInputElement;
    const newPassword = document.getElementById('newPassword') as HTMLInputElement;
    const confirmNewPassword = document.getElementById('confirmNewPassword') as HTMLInputElement;

    if (oldPassword !== null && newPassword !== null && confirmNewPassword !== null) {
        const input: ChangePasswordDto = {
            oldPassword: oldPassword.value,
            newPassword: newPassword.value,
            confirmNewPassword: confirmNewPassword.value,
        };

        http.post<ServerResponse<null>>(routers.user.changePassword, input);
    }
});
