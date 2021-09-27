import { debounce } from '../helper/debounce';
import { loadingComponent } from './loading';

const deleteIcon = `<svg viewBox="64 64 896 896" focusable="false" data-icon="close" width="1em" height="1em" fill="currentColor" aria-hidden="true">
                <path d="M563.8 512l262.5-312.9c4.4-5.2.7-13.1-6.1-13.1h-79.8c-4.7 0-9.2 2.1-12.3 5.7L511.6 449.8 295.1 191.7c-3-3.6-7.5-5.7-12.3-5.7H203c-6.8 0-10.5 7.9-6.1 13.1L459.4 512 196.9 824.9A7.95 7.95 0 00203 838h79.8c4.7 0 9.2-2.1 12.3-5.7l216.5-258.1 216.5 258.1c3 3.6 7.5 5.7 12.3 5.7h79.8c6.8 0 10.5-7.9 6.1-13.1L563.8 512z"></path>
            </svg>`;

export const getResultItemComponent = (label: string, callback: () => void) => {
    const listItem = document.createElement('li');
    listItem.classList.add('p-1', 'fade-in');
    const button = document.createElement('button');

    button.classList.add(
        'block',
        'w-full',
        'search-item',
        'outline-none',
        'p-1',
        'text-left',
        'duration-300',
        'hover:bg-tango-200',
        'focus:outline-none'
    );
    button.addEventListener('click', callback);
    button.setAttribute('type', 'button');
    button.innerText = label;
    listItem.appendChild(button);

    return listItem;
};

export const getBadgeComponent = (name: string, callback: () => void) => {
    const button = document.createElement('button');
    button.classList.add('flex', 'space-x-0.5', 'p-1', 'bg-gray-200', 'rounded-sm', 'items-center', 'hover:bg-gray-300', 'duration-300', 'm-0.5');
    button.addEventListener('click', () => {
        callback();
    });
    const label = document.createElement('span');
    label.textContent = name;
    const icon = document.createElement('span');
    icon.innerHTML = deleteIcon;

    button.appendChild(label);
    button.appendChild(icon);

    return button;
};

export function handleSelectBadge(id: string, callback: () => Promise<string[]>, delayTime: number = 500) {
    const searchResult = document.getElementById(`${id}Result`);
    const inputElement = document.getElementById(id);

    //handle null
    if (inputElement !== null && searchResult !== null) {
        //detect click outside
        let isWithin = false;

        const handleOnClickOutside = () => {
            if (!isWithin) searchResult?.classList.add('hidden');
        };

        inputElement?.addEventListener('focus', () => {
            searchResult?.classList.remove('hidden');
            document.removeEventListener('click', handleOnClickOutside);
            searchResult.addEventListener('mouseenter', () => {
                isWithin = true;
            });
        });

        inputElement?.addEventListener('blur', () => {
            if (!isWithin) searchResult?.classList.add('hidden');
            searchResult.addEventListener('mouseleave', () => {
                isWithin = false;
            });
            document.addEventListener('click', handleOnClickOutside);
        });

        // handle select drop list
        let currentSelect = -1;
        let currentLabel: string | null = null;

        inputElement?.addEventListener('keyup', function (event: KeyboardEvent) {
            if (event.code === 'Enter' && currentLabel !== null) {
                const input = inputElement as HTMLInputElement;
                input.value = currentLabel;
            } else if (event.code === 'ArrowUp' || event.code === 'ArrowDown') {
                const searchItems = searchResult.getElementsByClassName('search-item');
                if (searchItems.length) {
                    if (event.code === 'ArrowDown') {
                        currentSelect += 1;
                        if (currentSelect > searchItems.length - 1) currentSelect = 0;
                    } else if (event.code === 'ArrowUp') {
                        currentSelect -= 1;
                        if (currentSelect <= -1) currentSelect = searchItems.length - 1;
                    }

                    for (let index = 0; index < searchItems.length; index++) {
                        const element = searchItems[index];
                        if (currentSelect === index) {
                            element.classList.add('bg-tango-200');

                            currentLabel = element.textContent;
                        } else {
                            element.classList.remove('bg-tango-200');
                        }
                    }
                }
            } else {
                debounce(delayTime, async () => {
                    currentSelect = -1;
                    currentLabel = null;
                    searchResult.innerHTML = '';
                    searchResult.innerHTML = loadingComponent;

                    const data = await callback();

                    const wrapper = document.createElement('div');
                    data.forEach((item) => {
                        const searchItem = getResultItemComponent(item, () => {
                            (inputElement as HTMLInputElement).value = item;
                            const searchItems = searchResult.getElementsByClassName('search-item');

                            for (let index = 0; index < searchItems.length; index++) {
                                const element = searchItems[index];
                                element.classList.remove('bg-tango-200');
                            }
                        });
                        wrapper.append(searchItem);
                    });
                    if (searchResult) {
                        searchResult.innerHTML = '';
                        searchResult.appendChild(wrapper);
                    }
                });
            }
        });
    }
}
