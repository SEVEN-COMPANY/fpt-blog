import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { pageChange } from '../package/helper/pagination';
pageChange('listTagForm');

const clearTagBtn = document.getElementById('clear-unused-tag');

clearTagBtn?.addEventListener('click', function () {
    http.put(routers.tag.clearUnused).then(() => {
        setTimeout(() => {
            window.location.reload();
        }, 1000);
    });
});
