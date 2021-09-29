var path = require('path');

module.exports = {
    entry: {
        'auth-login': ['./src/auth/login.ts'],
        'auth-register': ['./src/auth/register.ts'],
        'auth-update': ['./src/auth/login.ts'],
        'user-update': ['./src/user/update.ts'],
        'user-changePassword': ['./src/user/changePassword.ts'],
        'category-create': ['./src/category/create.ts'],
        'category-update': ['./src/category/update.ts'],
        'category-list': ['./src/category/list.ts'],
        'blog-create': ['./src/blog/create.ts'],
        'blog-editor': ['./src/blog/editor.ts'],
    },
    output: {
        path: __dirname + '/../wwwroot/js',
        filename: '[name].js',
    },
    resolve: {
        alias: {
            parchment: path.resolve(__dirname, 'node_modules/parchment/src/parchment.ts'),
            quill$: path.resolve(__dirname, 'node_modules/quill/quill.js'),
        },
        extensions: ['.js', '.ts', '.svg'],
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                use: [
                    {
                        loader: 'babel-loader',
                        options: {
                            presets: ['es2015'],
                        },
                    },
                ],
            },
            {
                test: /\.ts$/,
                use: [
                    {
                        loader: 'ts-loader',
                        options: {
                            compilerOptions: {
                                declaration: false,
                                target: 'es5',
                                module: 'commonjs',
                            },
                            transpileOnly: true,
                        },
                    },
                ],
            },
            {
                test: /\.svg$/,
                use: [
                    {
                        loader: 'html-loader',
                        options: {
                            minimize: true,
                        },
                    },
                ],
            },
        ],
    },
};
