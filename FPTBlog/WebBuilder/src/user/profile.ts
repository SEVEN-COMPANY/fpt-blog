import { http } from '../package/axios';
import { routers } from '../package/axios/routes';

const followBtn = document.getElementById('follow-btn');

followBtn?.addEventListener('click', function () {
    const userId = followBtn.getAttribute('data-userId');

    console.log(routers.user.follow);
    if (userId) {
        http.post(`${routers.user.follow}?followerId=${userId}`);
    }
});
