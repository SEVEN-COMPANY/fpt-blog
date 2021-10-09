import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

const createPostForm = document.getElementById('createNewPost');
createPostForm?.addEventListener('click', function (event: Event) {
    http.post<ServerResponse<null>>(routers.post.create).then((res) => {
        window.location.reload();
    });
});

const deletePost = document.getElementsByClassName('delete-post');
for (let index = 0; index < deletePost.length; index++) {
    const element = deletePost[index] as HTMLButtonElement;

    element.addEventListener('click', function () {
        const isSend = confirm('Are you sure to delete this draft?');
        if (isSend) {
            const postId = element.getAttribute('data-postId');
            http.put<ServerResponse<null>>(routers.post.deletePost, { PostId: postId }).then((res) => {
                setTimeout(() => {
                    window.location.reload();
                }, 1000);
            });
        }
    });
}
