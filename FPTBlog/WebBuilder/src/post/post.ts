import { http } from '../package/axios';
import { routers } from '../package/axios/routes';

interface PostLikeOrDislike {
    postId: string;
}

const likeCount = document.getElementById('like-count');
const dislikeCount = document.getElementById('dislike-count');
const likeBtn = document.getElementById('post-like');
const dislikeBtn = document.getElementById('post-dislike');
const postId = document.getElementById('postId') as HTMLInputElement;

likeBtn?.addEventListener('click', function () {
    const input: PostLikeOrDislike = {
        postId: postId.value,
    };

    http.post(routers.post.likePost, input).then(({ data }) => {
        const like: string = data.data.like ?? '';
        const disLike: string = data.data.dislike ?? '';
        if (likeCount) likeCount.innerText = like;
        if (dislikeCount) dislikeCount.innerText = disLike;
    });
});
dislikeBtn?.addEventListener('click', function () {
    const input: PostLikeOrDislike = {
        postId: postId.value,
    };

    http.post(routers.post.dislikePost, input).then(({ data }) => {
        const like: string = data.data.like ?? '';
        const disLike: string = data.data.dislike ?? '';
        if (likeCount) likeCount.innerText = like;
        if (dislikeCount) dislikeCount.innerText = disLike;
    });
});
