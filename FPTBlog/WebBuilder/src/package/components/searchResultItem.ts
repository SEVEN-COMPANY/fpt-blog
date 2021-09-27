const hello = `<li class="">
                <button class="block w-full p-1 text-left duration-300 hover:bg-tango-200">
                    Hello
                </button>
            </li>`;

export const getResultItemComponent = (label: string, callback: () => void) => {
    const listItem = document.createElement('li');

    const button = document.createElement('button');
    button.addEventListener('click', callback);
    button.innerText = label;
    listItem.appendChild(button);

    return listItem;
};
