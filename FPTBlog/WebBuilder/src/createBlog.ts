import { editor } from './package/quill';
import { saveToServer, selectLocalImage } from './package/quill/helper';
import { http } from './package/axios';

window.onload = async function () {
    editor.getModule('toolbar').addHandler('image', () => {
        selectLocalImage(editor, saveToServer);
    });
    const btnSend = document.getElementById('send');
    const btnSave = document.getElementById('save');

    btnSend?.addEventListener('click', function () {
        const blogId = document.getElementById('blogId');
        if (blogId) {
            http.post('/blog', {
                BlogId: blogId.innerHTML.trim(),
                Title: 'hello',
                Content: editor.root.innerHTML,
            });
        }
    });

    btnSave?.addEventListener('click', function () {
        const blogId = document.getElementById('blogId');
        if (blogId) {
            http.post('/blog/save', {
                BlogId: blogId.innerHTML.trim(),
                Title: 'hello',
                Content: editor.root.innerHTML,
            });
        }
    });
};
