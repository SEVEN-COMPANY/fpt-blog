export const slideOver = (id: string) => {
    const btn = document.getElementById(`${id}-btn`);
    const btnClose = document.getElementById(`${id}-btn-close`);

    const wrapper = document.getElementById(`${id}-wrapper`);
    const bg = document.getElementById(`${id}-bg`);
    const panel = document.getElementById(`${id}-panel`);

    const modalToggle = () => {
        wrapper?.classList.add('invisible');
    };

    btn?.addEventListener('click', function () {
        wrapper?.classList.remove('invisible');
        bg?.classList.add('opacity-100');
        bg?.classList.remove('opacity-0');
        panel?.classList.add('translate-x-0');
        panel?.classList.remove('translate-x-full');
        panel?.removeEventListener('transitionend', modalToggle);
    });

    btnClose?.addEventListener('click', function () {
        bg?.classList.remove('opacity-100');
        bg?.classList.add('opacity-0');
        panel?.classList.remove('translate-x-0');
        panel?.classList.add('translate-x-full');
        panel?.addEventListener('transitionend', modalToggle);
    });
};
