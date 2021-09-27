import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

const createBlogForm = document.getElementById('createNewPost');
createBlogForm?.addEventListener('click', function (event: Event) {
    http.post<ServerResponse<null>>(routers.blog.create).then((res) => {
        console.log(res.data);
    });
});
