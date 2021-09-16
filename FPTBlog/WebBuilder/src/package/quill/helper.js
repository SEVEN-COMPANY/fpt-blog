"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.saveToServer = exports.selectLocalImage = exports.insertToEditor = void 0;
var axios_1 = require("../axios");
function insertToEditor(editor, url) {
    var range = editor.getSelection();
    if (range)
        editor.insertEmbed(range.index, 'image', "" + url);
}
exports.insertToEditor = insertToEditor;
function selectLocalImage(editor, cb) {
    var input = document.createElement('input');
    input.setAttribute('type', 'file');
    input.click();
    input.onchange = function () {
        if ((input === null || input === void 0 ? void 0 : input.files) && input.files[0]) {
            var file = input.files[0];
            if (/^image\//.test(file.type)) {
                cb(editor, file);
            }
            else {
                alert('You could only upload images');
            }
        }
    };
}
exports.selectLocalImage = selectLocalImage;
function saveToServer(editor, file) {
    var fd = new FormData();
    fd.append('image', file);
    var formData = new FormData();
    formData.append('image', file);
    axios_1.http.post('/blog/image', formData).then(function (res) {
        var imageUrl = res.data;
        insertToEditor(editor, imageUrl);
    });
}
exports.saveToServer = saveToServer;
