export const pageChange = (formId: string) => {
    const paginationSize = document.getElementById('pagination-size') as HTMLSelectElement;
    const paginationBtn = document.getElementById('pagination-btn');

    paginationSize?.addEventListener('change', function (_) {
        var option = paginationSize.options[paginationSize.selectedIndex];

        const pageSizeInput = document.getElementById('pageSize') as HTMLInputElement;
        pageSizeInput.value = option.value;

        const pageIndexInput = document.getElementById('pageIndex') as HTMLInputElement;
        pageIndexInput.value = '0';

        const form = document.getElementById(formId) as HTMLFormElement;
        form.submit();
    });

    const pageBtn = paginationBtn?.getElementsByTagName('button');
    if (pageBtn) {
        for (let index = 0; index < pageBtn.length; index++) {
            const element = pageBtn[index];
            element.addEventListener('click', function (_) {
                const pageIndexInput = document.getElementById('pageIndex') as HTMLInputElement;

                const value = element.getAttribute('data-index');
                if (value) {
                    pageIndexInput.value = value;
                }
                const form = document.getElementById(formId) as HTMLFormElement;
                form.submit();
            });
        }
    }
};
