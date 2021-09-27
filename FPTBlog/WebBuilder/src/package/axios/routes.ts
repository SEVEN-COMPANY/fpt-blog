export const routerLinks = {
    home: '/',
    loginForm: '/auth/login',
};
export const routers = {
    category: {
        create: '/api/category',
        update: '/api/category/update',
    },
    blog: {
        create: '/api/blog',
        addNewTagToBlog: '/api/blog/tag',
        getTagOfBlog: (blogId: string) => `/api/blog/tag?blogId=${blogId}`,
    },
    user: {
        changePassword: '/api/user/change-password',
        update: '/api/user',
    },
    tag: {
        getAll: '/api/tag/all',
        getByName: (name: string) => `/api/tag?name=${name}`,
    },
    loginUser: '/api/auth/login',
    getUser: '/api/user',
    registerUser: '/api/auth/register',
    saveBlog: '/api/blog/save',
    uploadImageBlog: '/api/blog/image',
};
