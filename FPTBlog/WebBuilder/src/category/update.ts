import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';

interface UpdateCategoryDto {
    categoryId: string;
    name: string;
    description: string;
    status: number;
}

let status = 1;

const createCategoryForm = document.getElementById('updateCategoryForm');
const statusList = document.querySelectorAll('input[name="status"]');
statusList.forEach((radio) => {
    const element = radio as HTMLInputElement;
    if (element.getAttribute('checked')) {
        status = Number(element.value);
    }
    radio.addEventListener('click', function () {
        status = Number(element.value);
    });
});

createCategoryForm?.addEventListener('submit', function (event: Event) {
    event.preventDefault();
    const name = document.getElementById('name') as HTMLInputElement;
    const description = document.getElementById('description') as HTMLInputElement;
    const categoryId = document.getElementById('categoryId') as HTMLInputElement;

    if (name != null && description != null && status != null) {
        const input: UpdateCategoryDto = {
            name: name.value,
            description: description.value,
            status: status,
            categoryId: categoryId.value,
        };
        http.put<ServerResponse<null>>(routers.category.update, input);
    }
});
