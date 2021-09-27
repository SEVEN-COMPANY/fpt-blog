import { editor } from '../package/quill';
import { saveToServer, selectLocalImage } from '../package/quill/helper';
import { http } from '../package/axios';
import { ServerResponse } from '../package/interface/serverResponse';
import { routers } from '../package/axios/routes';
import { handleSelectBadge } from '../package/components/listBadge';

interface CreateNewBlogDto {
    title: string;
    content: string;
}

interface Tag {
    tagId: string;
    name: string;
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
        http.post<ServerResponse<null>>(routers.createBlog, input).then(() => {});
    }
});

handleSelectBadge('tag', async () => {
    const { data } = await http.get<ServerResponse<{ quantity: number; tag: Tag }[]>>(routers.tag.getAll);
    return data.data.slice(0, 10).map((item) => item.tag.name);
});
