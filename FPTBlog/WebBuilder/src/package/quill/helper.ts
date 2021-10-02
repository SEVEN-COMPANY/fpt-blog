import Quill from 'quill';
import { http } from '../axios';
import { routers } from '../axios/routes';
import { ServerResponse } from '../interface/serverResponse';
// let coverUrl = '';
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
export async function saveToServer(editor: Quill, file: File) {
    const fd = new FormData();

    fd.append('image', file);
    const formData = new FormData();
    formData.append('image', file);
    http.post<ServerResponse<string>>(routers.post.uploadImagePost, formData)
        .then((res) => {
            const imageUrl = res.data.data;
            // if (coverUrl) coverUrl = imageUrl;
            insertToEditor(editor, imageUrl);
        })
        .catch((error) => {
            console.log(error);
        });
}

// export { coverUrl };
