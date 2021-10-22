import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

const btnClose = document.getElementById('modal-btn-close-remove');
const wrapper = document.getElementById('modal-wrapper-remove');
const bg = document.getElementById('modal-bg-remove');
const panel = document.getElementById('modal-panel-remove');
const btnAccept = document.getElementById('modal-btn-accept-remove');
const btnCancel = document.getElementById('modal-btn-cancel-remove');

interface RemoveUserRewardDto {
    userId: string;
    rewardId: string;
}

const rows = document.getElementsByTagName('tr');
let userId: any = null;
let rewardId: any = null;
const modalToggle = () => {
    wrapper?.classList.add('invisible');
};

for (let index = 0; index < rows.length; index++) {
    const element = rows[index] as HTMLTableRowElement;
    const btns = element.getElementsByClassName('modal-btn');
    for (let index = 0; index < btns.length; index++) {
        const btn = btns[index] as HTMLButtonElement;

        if (btn)
            btn.addEventListener('click', function () {
                wrapper?.classList.remove('invisible');
                bg?.classList.add('opacity-100');
                bg?.classList.remove('opacity-0');
                panel?.classList.add('opacity-100', 'translate-y-0', 'sm:scale-100');
                panel?.classList.remove('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
                panel?.removeEventListener('transitionend', modalToggle);

                userId = btn.getAttribute('data-userId');
                rewardId = btn.getAttribute('data-rewardId');
            });
    }
}

btnAccept?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);

    if (userId !== null && rewardId !== null) {
        const input: RemoveUserRewardDto = {
            userId: userId,
            rewardId: rewardId,
        };

        http.put<ServerResponse<null>>(routers.reward.removeUserReward, input).then(() => {
            setTimeout(() => {
                window.location.reload();
            }, 700);
        });
    }
});

btnClose?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);
});

btnCancel?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);
});
