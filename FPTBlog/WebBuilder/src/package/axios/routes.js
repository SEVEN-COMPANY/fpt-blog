"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.routers = exports.routerLinks = void 0;
exports.routerLinks = {
    home: '/',
    loginForm: '/auth/login',
};
exports.routers = {
    category: {
        create: '/api/category',
        update: '/api/category/update',
    },
    loginUser: '/api/auth/login',
    getUser: '/api/user',
    registerUser: '/api/auth/register',
    createBlog: '/api/blog',
    uploadImageBlog: '/api/blog/image',
};
