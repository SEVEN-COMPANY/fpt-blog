import { editor } from '../package/quill';
import { saveToServer, selectLocalImage } from '../package/quill/helper';
import { http } from '../package/axios';
import { ServerResponse } from '../package/interface/serverResponse';
import { routers } from '../package/axios/routes';
import { debounce } from '../package/helper/debounce';
import { getResultItemComponent } from '../package/components/searchResultItem';

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
        http.post<ServerResponse<null>>(routers.createBlog, input).then(() => {
            console.log('hello');
        });
    }
});

const tag = document.getElementById('tag');

tag?.addEventListener('keyup', function () {
    debounce(500, () => {
        const wrapper = document.createElement('div');

        http.get<ServerResponse<{ quantity: number; tag: Tag }[]>>(routers.tag.getAll).then(({ data }) => {
            console.log('hello');
            data.data.slice(0, 10).forEach((item) => {
                const searchItem = getResultItemComponent(item.tag.name, () => {
                    console.log('hello');
                });

                wrapper.append(searchItem);
            });
            const searchResult = document.getElementById('tagResult');
            if (searchResult) {
                searchResult.innerHTML = '';
                searchResult.appendChild(wrapper);
            }
        });
    });
});
