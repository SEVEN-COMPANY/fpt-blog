import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { pageChange } from '../package/helper/pagination';
import { ApexOptions } from 'apexcharts';
import { ServerResponse } from '../package/interface/serverResponse';
pageChange('listPostForm');

interface ApprovedPostDto {
    postId: string;
    note: string;
    status: string;
}

const btn = document.getElementsByClassName(`modal-btn`);
const btnClose = document.getElementById(`modal-btn-close`);

const wrapper = document.getElementById(`modal-wrapper`);
const bg = document.getElementById(`modal-bg`);
const panel = document.getElementById(`modal-panel`);

const modalToggle = () => {
    wrapper?.classList.add('invisible');
};

for (let index = 0; index < btn.length; index++) {
    const element = btn[index];
    element?.addEventListener('click', function () {
        wrapper?.classList.remove('invisible');
        bg?.classList.add('opacity-100');
        bg?.classList.remove('opacity-0');
        panel?.classList.add('translate-x-0');
        panel?.classList.remove('translate-x-full');
        panel?.removeEventListener('transitionend', modalToggle);
        const postId = element.getAttribute('data-postId');

        const title = element.getAttribute('data-title');
        const postName = document.getElementById('postName');
        const postLink = document.getElementById('postLink');
        if (title && postName) postName.innerText = title;
        if (postLink && postId) postLink.setAttribute('href', `/post?postId=${postId}`);
        if (postId) {
            const approvedPostForm = document.getElementById('approvedPostForm');
            approvedPostForm?.addEventListener('submit', function (event) {
                const approvedStatus = document.getElementById('approvedStatus') as HTMLSelectElement;
                const note = document.getElementById('note') as HTMLInputElement;
                event.preventDefault();

                const isView = confirm('Remember read the post before submitting');
                if (isView) {
                    const input: ApprovedPostDto = {
                        note: note.value,
                        status: approvedStatus.value,
                        postId: postId,
                    };
                    http.post(routers.post.approvedPost, input).then(() => {
                        setTimeout(() => {
                            window.location.reload();
                        }, 1000);
                    });
                }
            });
        }
    });
}

btnClose?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('translate-x-0');
    panel?.classList.add('translate-x-full');
    panel?.addEventListener('transitionend', modalToggle);
});

let chart: ApexCharts;
let chart2: ApexCharts;
let chart3: ApexCharts;
let chart4: ApexCharts;
let isClick = false;
const chartFilter = document.getElementById('chart-filter');
const buildChart = (labels: string[], posts: number[], views: number[], interacts: number[], users: number[]) => {
    const chartElement = document.getElementById('chart');
    const chartElement2 = document.getElementById('chart2');
    const chartElement3 = document.getElementById('chart3');
    const chartElement4 = document.getElementById('chart4');

    const commonOption: ApexOptions = {
        chart: {
            id: 'fb',
            group: 'social',
            type: 'line',
            height: 160,
        },

        yaxis: {
            labels: {
                minWidth: 40,
            },
        },
        tooltip: {},
        labels: labels,
    };

    const option1: ApexOptions = {
        series: [
            {
                name: 'Number of post',
                data: posts,
            },
        ],
        title: { text: 'Post', align: 'center' },
        colors: ['#008FFB'],
        ...commonOption,
    };
    const option2: ApexOptions = {
        series: [
            {
                name: 'Number of view',
                data: views,
            },
        ],
        title: { text: 'View', align: 'center' },
        colors: ['#e74c3c'],
        ...commonOption,
    };
    const option3: ApexOptions = {
        series: [
            {
                name: 'Number of interact',
                data: interacts,
            },
        ],
        title: { text: 'Interact', align: 'center' },
        colors: ['#16a085'],
        ...commonOption,
    };
    const option4: ApexOptions = {
        series: [
            {
                name: 'Number of user',
                data: users,
            },
        ],
        title: { text: 'User', align: 'center' },
        colors: ['#e67e22'],
        ...commonOption,
    };

    if (chart && chart2 && chart3 && chart4) {
        chart.destroy();
        chart2.destroy();
        chart3.destroy();
        chart4.destroy();
    }

    chart = new ApexCharts(chartElement, option1);
    chart2 = new ApexCharts(chartElement2, option2);
    chart3 = new ApexCharts(chartElement3, option3);
    chart4 = new ApexCharts(chartElement4, option4);

    chart.render();
    chart2.render();
    chart3.render();
    chart4.render();
};

const chartFilterBtn = document.getElementById('chart-filter-btn');
const viewChart = document.getElementById('view-chart');
viewChart?.addEventListener('click', function () {
    const label = viewChart?.getElementsByTagName('span')[0] as HTMLSpanElement;
    chartFilter?.classList.toggle('hidden');
    http.get<ServerResponse<{ date: string; post: number; view: number; interact: number; user: number }[]>>(routers.post.chart).then(({ data }) => {
        const labels = data.data.map((item) => item.date);
        const posts = data.data.map((item) => item.post);
        const views = data.data.map((item) => item.view);
        const interacts = data.data.map((item) => item.interact);
        const users = data.data.map((item) => item.user);
        buildChart(labels, posts, views, interacts, users);
        if (!isClick) {
            isClick = true;
            label.innerText = 'Close Chart';
        } else {
            chart.destroy();
            chart2.destroy();
            chart3.destroy();
            chart4.destroy();
            isClick = false;
            label.innerText = 'View Chart';
        }
    });
});

chartFilterBtn?.addEventListener('click', function () {
    const startDate = document.getElementById('startDate') as HTMLInputElement;
    const endDate = document.getElementById('endDate') as HTMLInputElement;
    const url = `${routers.post.chart}?fromDate=${startDate.value}&toDate=${endDate.value}`;
    http.get<ServerResponse<{ date: string; post: number; view: number; interact: number; user: number }[]>>(url).then(({ data }) => {
        const labels = data.data.map((item) => item.date);
        const posts = data.data.map((item) => item.post);
        const views = data.data.map((item) => item.view);
        const interacts = data.data.map((item) => item.interact);
        const users = data.data.map((item) => item.user);
        buildChart(labels, posts, views, interacts, users);
    });
});
