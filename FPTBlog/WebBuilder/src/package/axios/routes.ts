export const routerLinks = {
    home: '/',
    loginForm: '/auth/login',
};
export const routers = {
    category: {
        create: '/api/category',
        update: '/api/category',
    },
    post: {
        create: '/api/post',
        addNewTagToPost: '/api/post/tag',
        getTagOfPost: (postId: string) => `/api/post/tag?postId=${postId}`,
        save: '/api/post/save',
        uploadImagePost: '/api/post/image',
        addCategoryToPost: '/api/post/category',
        likePost: '/api/post/like',
        dislikePost: '/api/post/dislike',
        sendPost: '/api/post/send',
        approvedPost: '/api/admin/post/approved',
        deletePost: '/api/post/delete',
    },
    user: {
        changePassword: '/api/user/change-password',
        update: '/api/user',
        get: '/api/user',
        status: '/api/admin/user/status',
        role: '/api/admin/user/role',
        follow: '/api/user/follow',
    },
    tag: {
        getAll: '/api/tag/all',
        clearUnused: '/api/tag/unused',
        getByName: (name: string) => `/api/tag?name=${name}`,
    },
    auth: {
        login: '/api/auth/login',
        register: '/api/auth/register',
    },
    reward: {
        create: '/api/reward',
        update: '/api/reward',
        getOne: '/api/reward',
        give: '/api/reward/give',
        delete: '/api/reward/delete',
    },
};
