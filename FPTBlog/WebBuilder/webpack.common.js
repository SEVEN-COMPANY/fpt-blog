var path = require('path');

module.exports = {
    entry: {
        createBlog: './src/createBlog.ts',
        navbar: './src/navbar.ts',
        google: './src/google.ts',
        login: './src/login.ts',
        register: './src/register.ts',
        common: './src/common.ts',
        createCategory: './src/category/create.ts',
        updateCategory: './src/category/update.ts',
        adminCommon: './src/adminCommon.ts',
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
