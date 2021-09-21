"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var quill_1 = require("./package/quill");
var helper_1 = require("./package/quill/helper");
var axios_1 = require("./package/axios");
var routes_1 = require("./package/axios/routes");
quill_1.editor.getModule('toolbar').addHandler('image', function () {
    helper_1.selectLocalImage(quill_1.editor, helper_1.saveToServer);
});
var createBlogForm = document.getElementById('createBlogForm');
createBlogForm === null || createBlogForm === void 0 ? void 0 : createBlogForm.addEventListener('submit', function (event) {
    event.preventDefault();
    var title = document.getElementById('title');
    if (title && quill_1.editor) {
        var input = {
            title: title.value,
            content: quill_1.editor.root.innerHTML,
        };
        axios_1.http.post(routes_1.routers.createBlog, input).then(function () {
            console.log('hello');
        });
    }
});
