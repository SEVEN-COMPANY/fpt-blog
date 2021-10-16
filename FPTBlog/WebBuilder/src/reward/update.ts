import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { previewImage } from '../package/helper/previewImage';

previewImage('file', 'preview-image');

const rewardForm = document.getElementById('updateRewardForm');
rewardForm?.addEventListener('submit', function (event) {
    event.preventDefault();

    const description = document.getElementById('description') as HTMLInputElement;
    const name = document.getElementById('name') as HTMLInputElement;
    const rewardId = document.getElementById('rewardId') as HTMLInputElement;
    const file = document.getElementById('file') as HTMLInputElement;
    const type = document.getElementById('type') as HTMLSelectElement;
    const constraint = document.getElementById('constraint') as HTMLInputElement;
    if (description != null && name != null && file != null) {
        const image = file.files ? file.files[0] : null;
        const fd = new FormData();

        fd.append('name', name.value);
        fd.append('rewardId', rewardId.value);
        fd.append('description', description.value);
        fd.append('type', type.value);
        fd.append('constraint', constraint.value);
        if (image) {
            fd.append('file', image);
        }
        http.put(routers.reward.create, fd);
    }
});
