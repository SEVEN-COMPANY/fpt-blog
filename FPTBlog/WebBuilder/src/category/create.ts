import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

interface CreateCategoryDto {
    name: string;
    description: string;
    status: number;
}

let status = 1;

const createCategoryForm = document.getElementById('createCategoryForm');
const statusList = document.querySelectorAll('input[name="status"]');
statusList.forEach((radio) => {
    radio.addEventListener('click', function () {
        status = Number((radio as HTMLInputElement).value);
    });
});

createCategoryForm?.addEventListener('submit', function (event: Event) {
    event.preventDefault();
    const name = document.getElementById('name') as HTMLInputElement;
    const description = document.getElementById('description') as HTMLInputElement;

    if (name != null && description != null && status != null) {
        const input: CreateCategoryDto = {
            name: name.value,
            description: description.value,
            status: status,
        };

        http.post<ServerResponse<null>>(routers.category.create, input).then(() => {
            name.value = '';
            description.value = '';
        });
    }
});
