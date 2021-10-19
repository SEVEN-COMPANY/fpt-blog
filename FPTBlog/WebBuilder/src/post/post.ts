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
interface User {
    avatarUrl: string;
    name: string;
    createDate: string;
    userId: string;
}
interface OriComment {
    commentId: string;
    content: string;
    createDate: string;
    dislike: number;
    like: number;
    oriCommentId: string;
    user: User;
}

interface Comment {
    oriComment: OriComment;
    subComments: OriComment[];
}

const commentBtn = document.getElementById('comment-btn');

commentBtn?.addEventListener('click', function () {
    const comment = document.getElementById('comment') as HTMLTextAreaElement;
    const postId = document.getElementById('postId') as HTMLInputElement;

    http.post(routers.comment.create, {
        content: comment.value,
        postId: postId.value,
    })
        .then(() => {
            initComment();
        })
        .finally(() => {
            comment.value = '';
        });
});

const initComment = () => {
    const postId = document.getElementById('postId') as HTMLInputElement;
    const commentWrapper = document.getElementById('comment-wrapper') as HTMLDivElement;
    const url = `${routers.comment.getComment}?postId=${postId.value}`;
    http.get<ServerResponse<User>>(routers.user.get).then(({ data }) => {
        http.get<ServerResponse<Comment[]>>(url).then(({ data: comments }) => {
            commentWrapper.innerHTML = '';

            comments.data.map((item) => {
                const mainComment = renderComment(item.oriComment, data.data);

                const subWrapper = document.createElement('div');
                subWrapper.classList.add('pl-2', 'lg:pl-4', 'lg:ml-4', 'border-l-2', 'border-tango-300');

                item.subComments.map((item2) => {
                    subWrapper.appendChild(renderComment(item2, data.data));
                });
                mainComment.appendChild(subWrapper);
                commentWrapper.appendChild(mainComment);
            });
        });
    });
};

initComment();

const renderTextarea = (content: string, callback: (value: string) => void) => {
    const textarea = document.createElement('textarea');
    textarea.classList.add('flex-1', 'border-none', 'outline-none', 'resize-none', 'focus:ring-transparent');
    textarea.value = content;

    const btnWrapper = document.createElement('div');
    const btn = document.createElement('button');
    btn.classList.add('p-2', 'font-semibold', 'text-tango-500');
    btn.innerText = 'Send';
    btn.addEventListener('click', function () {
        callback(textarea.value);
    });
    btnWrapper.appendChild(btn);

    const wrapper = document.createElement('div');
    wrapper.classList.add('flex', 'py-2', 'border-b', 'border-tango-300', 'fade-in');

    const closeBtn = document.createElement('button');
    closeBtn.innerHTML = `<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd" clip-rule="evenodd" d="M4.29289 4.29289C4.68342 3.90237 5.31658 3.90237 5.70711 4.29289L10 8.58579L14.2929 4.29289C14.6834 3.90237 15.3166 3.90237 15.7071 4.29289C16.0976 4.68342 16.0976 5.31658 15.7071 5.70711L11.4142 10L15.7071 14.2929C16.0976 14.6834 16.0976 15.3166 15.7071 15.7071C15.3166 16.0976 14.6834 16.0976 14.2929 15.7071L10 11.4142L5.70711 15.7071C5.31658 16.0976 4.68342 16.0976 4.29289 15.7071C3.90237 15.3166 3.90237 14.6834 4.29289 14.2929L8.58579 10L4.29289 5.70711C3.90237 5.31658 3.90237 4.68342 4.29289 4.29289Z" fill="#111827"></path>
                                </svg>`;
    closeBtn.addEventListener('click', function () {
        wrapper.remove();
    });

    wrapper.appendChild(closeBtn);
    wrapper.appendChild(textarea);
    wrapper.appendChild(btnWrapper);

    return wrapper;
};

const renderComment = (data: OriComment, user: User) => {
    const wrapper = document.createElement('div');
    wrapper.classList.add('space-y-4', 'fade-in');
    const btnWrapper = document.createElement('div');

    if (user.userId === data.user.userId) {
        const deleteBtn = document.createElement('button');
        deleteBtn.innerHTML = `<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path fill-rule="evenodd" clip-rule="evenodd" d="M4.29289 4.29289C4.68342 3.90237 5.31658 3.90237 5.70711 4.29289L10 8.58579L14.2929 4.29289C14.6834 3.90237 15.3166 3.90237 15.7071 4.29289C16.0976 4.68342 16.0976 5.31658 15.7071 5.70711L11.4142 10L15.7071 14.2929C16.0976 14.6834 16.0976 15.3166 15.7071 15.7071C15.3166 16.0976 14.6834 16.0976 14.2929 15.7071L10 11.4142L5.70711 15.7071C5.31658 16.0976 4.68342 16.0976 4.29289 15.7071C3.90237 15.3166 3.90237 14.6834 4.29289 14.2929L8.58579 10L4.29289 5.70711C3.90237 5.31658 3.90237 4.68342 4.29289 4.29289Z" fill="#111827"></path>
                                    </svg>`;
        deleteBtn.addEventListener('click', function () {
            const isConfirm = confirm('Do you want to delete this comment');
            if (isConfirm)
                http.put(routers.comment.deleteComment, { commentId: data.commentId }).then(() => {
                    initComment();
                });
        });
        btnWrapper.appendChild(deleteBtn);
    }

    const likeBtn = document.createElement('button');
    likeBtn.innerHTML = `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                              <path d="M7.48047 18.35L10.5805 20.75C10.9805 21.15 11.8805 21.35 12.4805 21.35H16.2805C17.4805 21.35 18.7805 20.45 19.0805 19.25L21.4805 11.95C21.9805 10.55 21.0805 9.34997 19.5805 9.34997H15.5805C14.9805 9.34997 14.4805 8.84997 14.5805 8.14997L15.0805 4.94997C15.2805 4.04997 14.6805 3.04997 13.7805 2.74997C12.9805 2.44997 11.9805 2.84997 11.5805 3.44997L7.48047 9.54997" stroke="#292D32" stroke-width="1.5" stroke-miterlimit="10"></path>
                              <path d="M2.37988 18.3499V8.5499C2.37988 7.1499 2.97988 6.6499 4.37988 6.6499H5.37988C6.77988 6.6499 7.37988 7.1499 7.37988 8.5499V18.3499C7.37988 19.7499 6.77988 20.2499 5.37988 20.2499H4.37988C2.97988 20.2499 2.37988 19.7499 2.37988 18.3499Z" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round">
                              </path>
                         </svg>`;
    likeBtn.addEventListener('click', function () {
        http.post(routers.comment.likeComment, { commentId: data.commentId }).then(() => {
            initComment();
        });
    });
    const likeLabel = document.createElement('span');
    likeLabel.classList.add('text-lg', 'font-medium', 'opacity-60', 'fade-in');
    likeLabel.innerText = String(data.like);

    const dislikeBtn = document.createElement('button');
    dislikeBtn.innerHTML = `<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                 <path d="M16.5197 5.6499L13.4197 3.2499C13.0197 2.8499 12.1197 2.6499 11.5197 2.6499H7.71973C6.51973 2.6499 5.21973 3.5499 4.91973 4.7499L2.51973 12.0499C2.01973 13.4499 2.91973 14.6499 4.41973 14.6499H8.41973C9.01973 14.6499 9.51973 15.1499 9.41973 15.8499L8.91973 19.0499C8.71973 19.9499 9.31973 20.9499 10.2197 21.2499C11.0197 21.5499 12.0197 21.1499 12.4197 20.5499L16.5197 14.4499" stroke="#292D32" stroke-width="1.5" stroke-miterlimit="10"></path>
                                 <path d="M21.6201 5.65V15.45C21.6201 16.85 21.0201 17.35 19.6201 17.35H18.6201C17.2201 17.35 16.6201 16.85 16.6201 15.45V5.65C16.6201 4.25 17.2201 3.75 18.6201 3.75H19.6201C21.0201 3.75 21.6201 4.25 21.6201 5.65Z" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round">
                                 </path>
                             </svg>`;

    dislikeBtn.addEventListener('click', function () {
        http.post(routers.comment.dislikeComment, { commentId: data.commentId }).then(() => {
            initComment();
        });
    });

    const dislikeLabel = document.createElement('span');
    dislikeLabel.classList.add('text-lg', 'font-medium', 'opacity-60', 'fade-in');
    dislikeLabel.innerText = String(data.dislike);

    btnWrapper.classList.add('flex', 'items-center', 'space-x-2');
    btnWrapper.appendChild(likeBtn);
    btnWrapper.appendChild(likeLabel);
    btnWrapper.appendChild(dislikeBtn);
    btnWrapper.appendChild(dislikeLabel);

    const userInfo = `
                     <div class="flex items-center space-x-2">
                            <div class="w-10 h-10 overflow-hidden rounded-full">
                                <img  src="${data.user.avatarUrl}" data-src="${data.user.avatarUrl}" alt="${data.user.name}" class="object-cover w-full h-full lazy">
                            </div>
                            <div>
                                <a class="font-semibold" href="/user/profile?userId=${data.user.userId}">${data.user.name}</a>
                                <p class="text-xs font-medium opacity-70">${data.createDate}</p>
                            </div>
                        </div>`;

    const btnContainer = document.createElement('div');
    btnContainer.classList.add('flex', 'justify-end');
    if (user.userId !== data.user.userId) {
        const replyBtn = document.createElement('button');
        replyBtn.classList.add('font-semibold', 'p-2');
        replyBtn.innerText = 'Reply';
        replyBtn.addEventListener('click', function () {
            const textarea = renderTextarea('', (value: string) => {
                http.post(routers.comment.create, {
                    content: value.replace(/\s{2,}/g, ' '),
                    postId: postId.value,
                    oriCommentId: data.oriCommentId ? data.oriCommentId : data.commentId,
                }).then(() => {
                    initComment();
                });
            });
            mainContent.appendChild(textarea);
        });
        btnContainer.appendChild(replyBtn);
    }

    const mainContent = document.createElement('div');
    mainContent.innerText = data.content;
    if (user.userId === data.user.userId) {
        const editBtn = document.createElement('button');
        editBtn.classList.add('font-semibold', 'p-2');
        editBtn.innerText = 'Edit';
        editBtn.addEventListener('click', function () {
            const textarea = renderTextarea(data.content, (value: string) => {
                http.put(routers.comment.updateComment, { commentId: data.commentId, content: value }).then(() => {
                    initComment();
                });
            });
            mainContent.appendChild(textarea);
        });
        btnContainer.appendChild(editBtn);
    }
    const contentWrapper = document.createElement('div');

    contentWrapper.appendChild(mainContent);

    contentWrapper.appendChild(btnContainer);

    const topWrapper = document.createElement('div');
    topWrapper.classList.add('flex', 'flex-col', 'lg:flex-row', 'lg:items-center', 'justify-between');
    topWrapper.innerHTML = userInfo;
    topWrapper.appendChild(btnWrapper);

    wrapper.appendChild(topWrapper);
    wrapper.appendChild(contentWrapper);

    return wrapper;
};
