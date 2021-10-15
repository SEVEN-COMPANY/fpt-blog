import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { pageChange } from '../package/helper/pagination';
import { ApexOptions } from 'apexcharts';
pageChange('listPostForm');

interface ApprovedPostDto {
    postId: string;
    note: string;
    status: string;
}

const btn = document.getElementsByClassName(`modal-btn`);
const btnClose = document.getElementById(`modal-btn-close`);

const wrapper = document.getElementById(`modal-wrapper`);
const bg = document.getElementById(`modal-bg`);
const panel = document.getElementById(`modal-panel`);

const modalToggle = () => {
    wrapper?.classList.add('invisible');
};

for (let index = 0; index < btn.length; index++) {
    const element = btn[index];
    element?.addEventListener('click', function () {
        wrapper?.classList.remove('invisible');
        bg?.classList.add('opacity-100');
        bg?.classList.remove('opacity-0');
        panel?.classList.add('translate-x-0');
        panel?.classList.remove('translate-x-full');
        panel?.removeEventListener('transitionend', modalToggle);
        const postId = element.getAttribute('data-postId');

        const title = element.getAttribute('data-title');
        const postName = document.getElementById('postName');
        const postLink = document.getElementById('postLink');
        if (title && postName) postName.innerText = title;
        if (postLink && postId) postLink.setAttribute('href', `/post?postId=${postId}`);
        if (postId) {
            const approvedPostForm = document.getElementById('approvedPostForm');
            approvedPostForm?.addEventListener('submit', function (event) {
                const approvedStatus = document.getElementById('approvedStatus') as HTMLSelectElement;
                const note = document.getElementById('note') as HTMLInputElement;
                event.preventDefault();

                const isView = confirm('Remember read the post before submitting');
                if (isView) {
                    const input: ApprovedPostDto = {
                        note: note.value,
                        status: approvedStatus.value,
                        postId: postId,
                    };
                    http.post(routers.post.approvedPost, input).then(() => {
                        setTimeout(() => {
                            window.location.reload();
                        }, 1000);
                    });
                }
            });
        }
    });
}

btnClose?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('translate-x-0');
    panel?.classList.add('translate-x-full');
    panel?.addEventListener('transitionend', modalToggle);
});
