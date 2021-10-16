import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { pageChange } from '../package/helper/pagination';
import { previewImage } from '../package/helper/previewImage';
import { slideOver } from '../package/modal';

pageChange('listRewardForm');
slideOver('modal');
previewImage('file', 'preview-image');

const rewardForm = document.getElementById('createRewardForm');
rewardForm?.addEventListener('submit', function (event) {
    event.preventDefault();

    const description = document.getElementById('description') as HTMLInputElement;
    const name = document.getElementById('name') as HTMLInputElement;
    const file = document.getElementById('file') as HTMLInputElement;
    const type = document.getElementById('type') as HTMLSelectElement;
    const constraint = document.getElementById('constraint') as HTMLInputElement;
    if (description != null && name != null && file != null) {
        const image = file.files ? file.files[0] : null;
        const fd = new FormData();

        fd.append('name', name.value);
        fd.append('type', type.value);
        fd.append('constraint', constraint.value);
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

const deleteBtn = document.getElementsByClassName('delete-btn');

for (let index = 0; index < deleteBtn.length; index++) {
    const btn = deleteBtn[index];
    btn.addEventListener('click', function () {
        const rewardId = btn.getAttribute('data-rewardId');
        const isSend = confirm('Are you to delete this badge?');
        if (isSend) {
            http.put(routers.reward.delete, { rewardId: rewardId }).then(() => {
                setTimeout(() => {
                    window.location.reload();
                }, 1000);
            });
        }
    });
}
