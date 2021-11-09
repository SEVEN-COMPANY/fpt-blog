import { http } from '../package/axios';
import { routerLinks, routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';
enum PostStatus {
    DRAFT = 1,
    WAIT = 2,
    APPROVED = 3,
    DENY = 4,
}
interface PostLikeOrDislike {
    postId: string;
}
interface PostChangeStatus {
    postId: string;
    status: PostStatus;
}

const likeCount = document.getElementById('like-count');
const dislikeCount = document.getElementById('dislike-count');
const likeBtn = document.getElementById('post-like');
const dislikeBtn = document.getElementById('post-dislike');
const postId = document.getElementById('postId') as HTMLInputElement;
const editPost = document.getElementById('editMyPost');
editPost?.addEventListener('click', function () {
    const input: PostChangeStatus = {
        postId: postId.value,
        status: PostStatus.DRAFT,
    };
    http.put(routers.post.changePostStatus, input).then(({ data }) => {
        const isCorrect = confirm('Are you sure to change this post?');
        if (isCorrect) {
            setTimeout(() => {
                window.location.assign(routerLinks.myDraftList);
            }, 700);
        }
    });
});

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
        deleteBtn.innerHTML = `<svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd" clip-rule="evenodd" d="M3.43431 3.43431C3.74673 3.12189 4.25326 3.12189 4.56568 3.43431L8 6.86863L11.4343 3.43431C11.7467 3.12189 12.2533 3.12189 12.5657 3.43431C12.8781 3.74673 12.8781 4.25326 12.5657 4.56568L9.13136 8L12.5657 11.4343C12.8781 11.7467 12.8781 12.2533 12.5657 12.5657C12.2533 12.8781 11.7467 12.8781 11.4343 12.5657L8 9.13136L4.56568 12.5657C4.25326 12.8781 3.74673 12.8781 3.43431 12.5657C3.12189 12.2533 3.12189 11.7467 3.43431 11.4343L6.86863 8L3.43431 4.56568C3.12189 4.25326 3.12189 3.74673 3.43431 3.43431Z" fill="#111827"/>
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
    likeBtn.innerHTML = `<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
<path d="M6.23373 15.2917L8.81709 17.2917C9.15043 17.625 9.90043 17.7917 10.4004 17.7917H13.5671C14.5671 17.7917 15.6504 17.0417 15.9004 16.0417L17.9004 9.95834C18.3171 8.79167 17.5671 7.79165 16.3171 7.79165H12.9838C12.4838 7.79165 12.0671 7.37498 12.1504 6.79165L12.5671 4.12498C12.7338 3.37498 12.2338 2.54165 11.4838 2.29165C10.8171 2.04165 9.98376 2.37498 9.65043 2.87498L6.23373 7.95832" stroke="#292D32" stroke-width="1.5" stroke-miterlimit="10"/>
<path d="M1.98323 15.2916V7.12491C1.98323 5.95825 2.48323 5.54158 3.6499 5.54158H4.48323C5.6499 5.54158 6.1499 5.95825 6.1499 7.12491V15.2916C6.1499 16.4582 5.6499 16.8749 4.48323 16.8749H3.6499C2.48323 16.8749 1.98323 16.4582 1.98323 15.2916Z" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
</svg>
`;
    likeBtn.addEventListener('click', function () {
        http.post(routers.comment.likeComment, { commentId: data.commentId }).then(() => {
            initComment();
        });
    });
    const likeLabel = document.createElement('span');
    likeLabel.classList.add('font-semibold', 'opacity-60', 'fade-in');
    likeLabel.innerText = String(data.like);

    const dislikeBtn = document.createElement('button');
    dislikeBtn.innerHTML = `<svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
<path d="M13.7664 4.70825L11.1831 2.70825C10.8497 2.37492 10.0997 2.20825 9.59975 2.20825H6.43311C5.43311 2.20825 4.34977 2.95825 4.09977 3.95825L2.09977 10.0416C1.68311 11.2083 2.43311 12.2083 3.68311 12.2083H7.01644C7.51644 12.2083 7.93311 12.6249 7.84977 13.2083L7.43311 15.8749C7.26644 16.6249 7.76644 17.4583 8.51642 17.7083C9.18308 17.9583 10.0164 17.6249 10.3497 17.1249L13.7664 12.0416" stroke="#292D32" stroke-width="1.5" stroke-miterlimit="10"/>
<path d="M18.0168 4.70833V12.875C18.0168 14.0417 17.5168 14.4583 16.3501 14.4583H15.5167C14.3501 14.4583 13.8501 14.0417 13.8501 12.875V4.70833C13.8501 3.54167 14.3501 3.125 15.5167 3.125H16.3501C17.5168 3.125 18.0168 3.54167 18.0168 4.70833Z" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
</svg>
`;

    dislikeBtn.addEventListener('click', function () {
        http.post(routers.comment.dislikeComment, { commentId: data.commentId }).then(() => {
            initComment();
        });
    });

    const dislikeLabel = document.createElement('span');
    dislikeLabel.classList.add('font-semibold', 'opacity-60', 'fade-in');
    dislikeLabel.innerText = String(data.dislike);

    btnWrapper.classList.add('flex', 'items-center', 'space-x-1');
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
        replyBtn.classList.add('font-semibold', 'p-2', 'text-sm');
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
        editBtn.classList.add('font-semibold', 'p-2', 'text-sm');
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
