import Quill from 'quill';
import { http } from '../axios';

export function insertToEditor(editor: Quill, url: string) {
    const range = editor.getSelection();
    if (range) editor.insertEmbed(range.index, 'image', `${url}`);
}

export function selectLocalImage(editor: Quill, cb: (editor: Quill, input: File) => void) {
    const input = document.createElement('input');
    input.setAttribute('type', 'file');
    input.click();
    input.onchange = () => {
        if (input?.files && input.files[0]) {
            const file = input.files[0];
            if (/^image\//.test(file.type)) {
                cb(editor, file);
            } else {
                alert('You could only upload images');
            }
        }
    };
}
export function saveToServer(editor: Quill, file: File) {
    const fd = new FormData();

    fd.append('image', file);
    const formData = new FormData();
    formData.append('image', file);

    http.post('/upload/image', formData)
        .then(() => {
            insertToEditor(editor, 'https://picsum.photos/200/300');
        })
        .finally(() => {
            insertToEditor(editor, 'https://picsum.photos/200/300');
        });
}
