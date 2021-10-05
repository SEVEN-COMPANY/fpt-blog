import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

interface BlockUserDto {
    userIdBlock: string;
}

const blockButton = document.getElementById('block-btn');
blockButton?.addEventListener('click', function (event: Event) {
    event.preventDefault();

    const userId = blockButton.getAttribute('userId');

    if (userId !== null) {
        const input: BlockUserDto = {
            userIdBlock: userId,
        };

        http.put<ServerResponse<null>>(routers.user.block, input);
    }
});
