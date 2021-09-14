import { editor } from './package/quill';
import { saveToServer, selectLocalImage } from './package/quill/helper';

window.onload = async function () {
    editor.getModule('toolbar').addHandler('image', () => {
        selectLocalImage(editor, saveToServer);
    });
    const btn = document.getElementById('send');

    btn?.addEventListener('click', function () {
        console.log(editor.root.innerHTML);
    });
};
