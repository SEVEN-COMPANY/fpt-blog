import { editor } from '../package/quill';

import { saveToServer, selectLocalImage } from '../package/quill/helper';
import { http } from '../package/axios';
import { ServerResponse } from '../package/interface/serverResponse';
import { routers } from '../package/axios/routes';
import { handleSelectBadge } from '../package/components/listBadge';
import { toastify } from '../package/toastify';

interface ToggleTagDto {
    postId: string;
    tagName: string;
}
interface SaveBlogDto {
    title: string;
    content: string;
    postId: string;
    readTime: number;
    coverUrl: string;
}
interface AddCategoryDto {
    categoryId: string;
    postId: string;
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
});

const saveChangePostBtn = document.getElementById('form-btn');

saveChangePostBtn?.addEventListener('click', function () {
    const title = document.getElementById('title') as HTMLInputElement;
    const postIdElement = document.getElementById('postId') as HTMLInputElement;
    const readTime = Math.ceil(editor.getText().split(' ').length / 250);

    const wrapperElement = document.createElement('div');
    wrapperElement.innerHTML = editor.root.innerHTML;
    let coverImage = 'https://picsum.photos/300';
    const imageElement = wrapperElement.getElementsByTagName('img');
    if (imageElement.length) {
        coverImage = imageElement[0].getAttribute('src') || 'https://picsum.photos/300';
    }

    if (title !== null && editor !== null && postIdElement !== null) {
        const input: SaveBlogDto = {
            title: title.value,
            content: editor.root.innerHTML,
            postId: postIdElement.value,
            readTime: readTime,
            coverUrl: coverImage,
        };

        console.log('hello');
        http.post<ServerResponse<null>>(routers.post.save, input)
            .then(() => {
                toastify({
                    text: 'Save your draft  successfully',
                    duration: 2000,
                    newWindow: true,
                    close: true,
                    gravity: 'top',
                    position: 'right',
                    backgroundColor: '#F37124',
                    stopOnFocus: true,
                });
            })
            .catch(() => {
                toastify({
                    text: 'Please check the field',
                    duration: 2000,
                    newWindow: true,
                    close: true,
                    gravity: 'top',
                    position: 'right',
                    backgroundColor: 'rgb(239, 68, 68)',
                    stopOnFocus: true,
                });
            });
    }
});

const categoryInput = document.getElementById('categoryId');

categoryInput?.addEventListener('change', function (event) {
    const postIdElement = document.getElementById('postId') as HTMLInputElement;
    const selectInput = event.currentTarget as HTMLSelectElement;
    const input: AddCategoryDto = {
        categoryId: selectInput.value,
        postId: postIdElement.value,
    };
    http.put<ServerResponse<Tag[]>>(routers.post.addCategoryToPost, input);
});

handleSelectBadge(
    'tag',
    async () => {
        const postIdElement = document.getElementById('postId') as HTMLInputElement;
        const { data } = await http.get<ServerResponse<Tag[]>>(routers.post.getTagOfPost(postIdElement.value));

        return data.data.map((item) => item.name);
    },

    async () => {
        const tagInputElement = document.getElementById('tag');

        if (tagInputElement) {
            const url = routers.tag.getByName((tagInputElement as HTMLInputElement).value);
            const { data } = await http.get<ServerResponse<Tag[]>>(url);

            return data.data.slice(0, 10).map((item) => item.name);
        }

        return [];
    },
    async (label: string) => {
        const postIdElement = document.getElementById('postId') as HTMLInputElement;

        if (postIdElement) {
            const input: ToggleTagDto = {
                postId: postIdElement.value,
                tagName: label,
            };

            const { data } = await http.post<ServerResponse<Tag[]>>(routers.post.addNewTagToPost, input);
            const tag = document.getElementById('tag') as HTMLInputElement;
            tag.value = '';
            return data.data.map((item) => item.name);
        }
        return [];
    },
    async (label: string) => {
        const postIdElement = document.getElementById('postId') as HTMLInputElement;

        const input: ToggleTagDto = {
            postId: postIdElement.value,
            tagName: label,
        };

        await http.put<ServerResponse<Tag[]>>(routers.post.addNewTagToPost, input);
        return;
    },
    500
);
