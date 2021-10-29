import { http } from '../package/axios';
import { routerLinks, routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

const btnRoleClose = document.getElementById(`modal-role-btn-close`);
const wrapperRole = document.getElementById(`modal-role-wrapper`);
const bgRole = document.getElementById(`modal-role-bg`);
const panelRole = document.getElementById(`modal-role-panel`);
const contentTitleUpgrade = document.getElementById(`modal-content-title-upgrade`);
const contentDescriptionUpgrade = document.getElementById(`modal-content-description-upgrade`);
const btnRoleAcceptUpgrade = document.getElementById(`modal-role-btn-accept-upgrade`);
const btnRoleCancel = document.getElementById(`modal-role-btn-cancel`);

interface LoginUserDto {
    username: string;
    password: string;
}

const loginForm = document.getElementById('loginForm');
loginForm?.addEventListener('submit', function (event: Event) {
    event.preventDefault();

    const username = document.getElementById('username') as HTMLInputElement;
    const password = document.getElementById('password') as HTMLInputElement;

    if (username !== null && password !== null) {
        const input: LoginUserDto = {
            username: username.value,
            password: password.value,
        };

        http.post<ServerResponse<null>>(routers.auth.login, input)
            .then(() => {
                setTimeout(() => {
                    window.location.assign(routerLinks.home);
                }, 700);
            })
            .catch(({ data }) => {
                const btn = document.getElementById('model-role-open');
                btn?.classList.add('hidden');
                if (data.data) {
                    btn?.classList.remove('hidden');
                }
            });
    } else {
        console.log('login form wrong');
    }
});

const btnActive = document.getElementById('model-role-open');
btnActive?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);
    wrapperRole?.classList.remove('invisible');
    bgRole?.classList.add('opacity-100');
    bgRole?.classList.remove('opacity-0');
    panelRole?.classList.add('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.remove('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.removeEventListener('transitionend', modalRoleToggle);
});

const modalRoleToggle = () => {
    wrapperRole?.classList.add('invisible');
    contentTitleUpgrade?.classList.add('hidden');
    contentDescriptionUpgrade?.classList.add('hidden');
    btnRoleAcceptUpgrade?.classList.add('hidden');
};
btnRoleAcceptUpgrade?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);
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
