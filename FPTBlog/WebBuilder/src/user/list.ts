import { pageChange } from '../package/helper/pagination';
import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';
// modal of user status
const btnClose = document.getElementById(`modal-btn-close`);
const wrapper = document.getElementById(`modal-wrapper`);
const bg = document.getElementById(`modal-bg`);
const panel = document.getElementById(`modal-panel`);
const btnAccept = document.getElementById(`modal-btn-accept`);
const btnCancel = document.getElementById(`modal-btn-cancel`);

// modal of user role
const btnRoleClose = document.getElementById(`modal-role-btn-close`);
const wrapperRole = document.getElementById(`modal-role-wrapper`);
const bgRole = document.getElementById(`modal-role-bg`);
const panelRole = document.getElementById(`modal-role-panel`);
const btnRoleAccept = document.getElementById(`modal-role-btn-accept`);
const btnRoleCancel = document.getElementById(`modal-role-btn-cancel`);

pageChange('listUserForm');

interface ToggleUserDto {
    userId: string;
}

const rows = document.getElementsByTagName('tr');
let userId: any = null;

const modalToggle = () => {
    wrapper?.classList.add('invisible');
};

for (let index = 0; index < rows.length; index++) {
    const element = rows[index] as HTMLTableRowElement;
    const btn = element.getElementsByClassName('modal-btn')[0] as HTMLButtonElement;
    if (btn)
        btn.addEventListener('click', function () {
            wrapper?.classList.remove('invisible');
            bg?.classList.add('opacity-100');
            bg?.classList.remove('opacity-0');
            panel?.classList.add('opacity-100', 'translate-y-0', 'sm:scale-100');
            panel?.classList.remove('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
            panel?.removeEventListener('transitionend', modalToggle);

            userId = btn.getAttribute('data-userId');
        });
}

btnAccept?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);

    if (userId !== null) {
        const input: ToggleUserDto = {
            userId: userId,
        };

        http.put<ServerResponse<null>>(routers.user.status, input);
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

// model role
const modalRoleToggle = () => {
    wrapperRole?.classList.add('invisible');
};

for (let index = 0; index < rows.length; index++) {
    const element = rows[index] as HTMLTableRowElement;
    const btn = element.getElementsByClassName('modal-role-btn')[0] as HTMLButtonElement;
    if (btn)
        btn.addEventListener('click', function () {
            wrapperRole?.classList.remove('invisible');
            bgRole?.classList.add('opacity-100');
            bgRole?.classList.remove('opacity-0');
            panelRole?.classList.add('opacity-100', 'translate-y-0', 'sm:scale-100');
            panelRole?.classList.remove('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
            panelRole?.removeEventListener('transitionend', modalRoleToggle);

            userId = btn.getAttribute('data-userId');
        });
}

btnRoleAccept?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);

    if (userId !== null) {
        const input: ToggleUserDto = {
            userId: userId,
        };

        http.put<ServerResponse<null>>(routers.user.role, input);
    }
});

btnRoleClose?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);
});

btnRoleCancel?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);
});
