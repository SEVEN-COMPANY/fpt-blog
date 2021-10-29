import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { pageChange } from '../package/helper/pagination';
import { ServerResponse } from '../package/interface/serverResponse';
pageChange('listNotificationForm');
interface User {
    userId: string;
    googleId: string;
    username: string;
    password: string;
    email: string;
    name: string;
    phone: string;
    address: string;
    avatarUrl: string;
    status: string;
    role: string;
    createDate: string;
}

interface Notification {
    notificationId: string;
    content: string;
    description: string;
    level: number;
    createDate: string;
    senderId: string;
    sender: User;
    receiverId: string;
    receiver: User;
}

const btn = document.getElementsByClassName('view-more');
const btnClose = document.getElementById(`modal-btn-close`);
const wrapper = document.getElementById(`modal-wrapper`);
const bg = document.getElementById(`modal-bg`);
const panel = document.getElementById(`modal-panel`);

const modalToggle = () => {
    wrapper?.classList.add('invisible');
};

for (let index = 0; index < btn.length; index++) {
    const element = btn[index] as HTMLButtonElement;

    if (element)
        element.addEventListener('click', function () {
            wrapper?.classList.remove('invisible');
            bg?.classList.add('opacity-100');
            bg?.classList.remove('opacity-0');
            panel?.classList.add('opacity-100', 'translate-y-0', 'sm:scale-100');
            panel?.classList.remove('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
            panel?.removeEventListener('transitionend', modalToggle);
        });
    element.addEventListener('click', function () {
        const notificationId = element.getAttribute('data-notificationId');
        const url = `${routers.notification.get}?notificationId=${notificationId}`;
        http.get<ServerResponse<Notification>>(url).then(({ data }) => {
            const notificationContent = document.getElementById('notification-content') as HTMLDivElement;
            const level =
                data.data.level === 1
                    ? `font-semibold text-sm text-green-500`
                    : data.data.level === 2
                    ? `font-semibold text-sm  text-yellow-500`
                    : `font-semibold text-sm  text-red-500`;

            const content = `
                    <div class="mt-2 space-y-4">
                        <div class="space-y-1">
                            <h4 class="text-sm font-semibold">Level</h4>
                            <p class="${level}">${data.data.level === 1 ? `Information` : data.data.level === 2 ? `Warning` : `Banned`}</p>

                        </div>
                        <div class="space-y-1">
                            <h4 class="text-sm font-semibold">Reciever</h4>
                            <p class="opacity-70">${data.data.receiver.name}</p>

                        </div>
                        <div class="space-y-1">
                            <h4 class="text-sm font-semibold">Sender</h4>
                            <p class="opacity-70">${data.data.sender.name}</p>
                        </div>
                        <div class="space-y-1">
                            <h4 class="text-sm font-semibold">Content</h4>
                            <p class="opacity-70">${data.data.content}</p>

                        </div>
                        <div class="space-y-1">
                            <h4 class="text-sm font-semibold">Description</h4>
                            <span class="text-xs font-semibold opacity-70">This message only appears with administrators</span>
                            <p class="opacity-70">${data.data.description}</p>
                        </div>
                    </div>`;
            notificationContent.innerHTML = content;
        });
    });
}

btnClose?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);
});
