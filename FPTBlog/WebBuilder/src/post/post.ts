import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

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

interface OriComment {
    commentId: string;
    content: string;
    createDate: string;
    dislike: number;
    like: number;
    user: {
        avatarUrl: string;
        name: string;
        createDate: string;
        userId: string;
    };
}

interface Comment {
    oriComment: OriComment;
    subComment: OriComment;
}

const commentBtn = document.getElementById('comment-btn');

commentBtn?.addEventListener('click', function () {
    const comment = document.getElementById('comment') as HTMLTextAreaElement;
    const postId = document.getElementById('postId') as HTMLInputElement;
    const commentWrapper = document.getElementById('comment-wrapper') as HTMLDivElement;
    http.post(routers.comment.create, {
        content: comment.value,
        postId: postId.value,
    }).then(() => {
        const url = `${routers.comment.getComment}?postId=${postId.value}`;

        http.get<ServerResponse<Comment[]>>(url).then(({ data }) => {
            console.log(data);

            const element = data.data.map((item) => renderComment(item.oriComment)).join('');
            commentWrapper.innerHTML = element;
        });
    });
});

const renderComment = (data: OriComment) => {
    return `<div class="space-y-4">
                <div class="flex items-center justify-between">
                    <div class="flex items-center space-x-2">
                        <div class="w-12 h-12 overflow-hidden rounded-full">
                            <img src="${data.user.avatarUrl}" data-src="${data.user.avatarUrl}" alt="${data.user.name}" class="object-cover w-full h-full lazy">
                        </div>
                        <div>
                            <a class="font-semibold" href="/user/profile?userId=${data.user.userId}">${data.user.name}</a>
                            <p class="text-xs font-medium opacity-70">${data.createDate}</p>
                        </div>
                    </div>

                    <div class="flex items-center space-x-2">
                        <button id="post-like">
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M7.48047 18.35L10.5805 20.75C10.9805 21.15 11.8805 21.35 12.4805 21.35H16.2805C17.4805 21.35 18.7805 20.45 19.0805 19.25L21.4805 11.95C21.9805 10.55 21.0805 9.34997 19.5805 9.34997H15.5805C14.9805 9.34997 14.4805 8.84997 14.5805 8.14997L15.0805 4.94997C15.2805 4.04997 14.6805 3.04997 13.7805 2.74997C12.9805 2.44997 11.9805 2.84997 11.5805 3.44997L7.48047 9.54997" stroke="#292D32" stroke-width="1.5" stroke-miterlimit="10"></path>
                                <path d="M2.37988 18.3499V8.5499C2.37988 7.1499 2.97988 6.6499 4.37988 6.6499H5.37988C6.77988 6.6499 7.37988 7.1499 7.37988 8.5499V18.3499C7.37988 19.7499 6.77988 20.2499 5.37988 20.2499H4.37988C2.97988 20.2499 2.37988 19.7499 2.37988 18.3499Z" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round">
                                </path>
                            </svg>
                        </button>
                        <span class="text-lg font-medium opacity-60 fade-in" id="like-count">${data.like}</span>
                        <button id="post-dislike">
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M16.5197 5.6499L13.4197 3.2499C13.0197 2.8499 12.1197 2.6499 11.5197 2.6499H7.71973C6.51973 2.6499 5.21973 3.5499 4.91973 4.7499L2.51973 12.0499C2.01973 13.4499 2.91973 14.6499 4.41973 14.6499H8.41973C9.01973 14.6499 9.51973 15.1499 9.41973 15.8499L8.91973 19.0499C8.71973 19.9499 9.31973 20.9499 10.2197 21.2499C11.0197 21.5499 12.0197 21.1499 12.4197 20.5499L16.5197 14.4499" stroke="#292D32" stroke-width="1.5" stroke-miterlimit="10"></path>
                                <path d="M21.6201 5.65V15.45C21.6201 16.85 21.0201 17.35 19.6201 17.35H18.6201C17.2201 17.35 16.6201 16.85 16.6201 15.45V5.65C16.6201 4.25 17.2201 3.75 18.6201 3.75H19.6201C21.0201 3.75 21.6201 4.25 21.6201 5.65Z" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round">
                                </path>
                            </svg>
                        </button>

                        <span class="text-lg font-medium opacity-60 fade-in" id="dislike-count">${data.dislike}</span>
                    </div>
                </div>
                <div class="space-y-2">
                    <p>${data.content}</p>
                    <p class="text-sm font-semibold text-right">Reply</p>
                </div>
            </div>`;
};
