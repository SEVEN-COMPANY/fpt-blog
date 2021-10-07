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
    },
    user: {
        changePassword: '/api/user/change-password',
        update: '/api/user',
        get: '/api/user',
        status: '/api/admin/user/status',
        role: '/api/admin/user/role',
    },
    tag: {
        getAll: '/api/tag/all',
        getByName: (name: string) => `/api/tag?name=${name}`,
    },
    auth: {
        login: '/api/auth/login',
        register: '/api/auth/register',
    },
};
