import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { pageChange } from '../package/helper/pagination';
import { slideOver } from '../package/modal';

pageChange('listTagForm');
slideOver('modal');

const rewardForm = document.getElementById('createRewardForm');
rewardForm?.addEventListener('submit', function (event) {
    event.preventDefault();

    const description = document.getElementById('description') as HTMLInputElement;
    const name = document.getElementById('name') as HTMLInputElement;
    const file = document.getElementById('file') as HTMLInputElement;
    if (description != null && name != null && file != null) {
        const image = file.files ? file.files[0] : null;
        const fd = new FormData();

        fd.append('name', name.value);
        fd.append('description', description.value);
        if (image) {
            fd.append('file', image);
        }
        http.post(routers.reward.create, fd).then(() => {
            setTimeout(() => {
                window.location.reload();
            }, 1000);
        });
    }
});
