import { http } from '../package/axios';
import { routers } from '../package/axios/routes';

interface PostLikeOrDislike {
    postId: string;
}

const count = document.getElementById('like-count');
const likeBtn = document.getElementById('post-like');
const dislikeBtn = document.getElementById('post-dislike');
const postId = document.getElementById('postId') as HTMLInputElement;

likeBtn?.addEventListener('click', function () {
    const input: PostLikeOrDislike = {
        postId: postId.value,
    };

    http.post(routers.post.likePost, input).then(({ data }) => {
        const like: string = data.data.like ?? '';
        if (count) count.innerText = like;
    });
});
dislikeBtn?.addEventListener('click', function () {
    const input: PostLikeOrDislike = {
        postId: postId.value,
    };

    http.post(routers.post.dislikePost, input).then(({ data }) => {
        const like: string = data.data.like ?? '';
        if (count) count.innerText = like;
    });
});
