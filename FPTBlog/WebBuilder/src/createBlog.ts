import { editor } from './package/quill';
import { saveToServer, selectLocalImage } from './package/quill/helper';
import { http } from './package/axios';
import { ServerResponse } from './package/interface/serverResponse';
import { routers } from './package/axios/routes';

interface CreateNewBlogDto {
    title: string;
    content: string;
}

editor.getModule('toolbar').addHandler('image', () => {
    selectLocalImage(editor, saveToServer);
});

const createBlogForm = document.getElementById('createBlogForm');
createBlogForm?.addEventListener('submit', function (event: Event) {
    event.preventDefault();

    const title = document.getElementById('title') as HTMLInputElement;
    if (title && editor) {
        const input: CreateNewBlogDto = {
            title: title.value,
            content: editor.root.innerHTML,
        };
        http.post<ServerResponse<null>>(routers.createBlog, input).then(() => {
            console.log('hello');
        });
    }
});
