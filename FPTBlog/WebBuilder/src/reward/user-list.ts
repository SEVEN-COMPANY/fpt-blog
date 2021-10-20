import { ApexOptions } from 'apexcharts';
import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { pageChange } from '../package/helper/pagination';
import { ServerResponse } from '../package/interface/serverResponse';

pageChange('listUserRewardForm');

const btnClose = document.getElementById(`modal-btn-close`);
const wrapper = document.getElementById(`modal-wrapper`);
const bg = document.getElementById(`modal-bg`);
const panel = document.getElementById(`modal-panel`);
const rewardId = document.getElementById('rewardId') as HTMLSelectElement;

interface Reward {
    rewardId: string;
    name: string;
    description: string;
    imageUrl: string;
    createDate: string;
}
const modalToggle = () => {
    wrapper?.classList.add('invisible');
};

const rewardBtn = document.getElementsByClassName('reward-btn');
for (let index = 0; index < rewardBtn.length; index++) {
    const btn = rewardBtn[index];
    btn.addEventListener('click', function () {
        wrapper?.classList.remove('invisible');
        bg?.classList.add('opacity-100');
        bg?.classList.remove('opacity-0');
        panel?.classList.add('translate-x-0');
        panel?.classList.remove('translate-x-full');
        panel?.removeEventListener('transitionend', modalToggle);

        const userId = btn.getAttribute('data-userid');
        const rewardForm = document.getElementById('giveRewardForm');
        rewardForm?.addEventListener('submit', function (event) {
            event.preventDefault();
            http.post(routers.reward.give, { userId: userId, rewardId: rewardId.value }).then(() => {
                setTimeout(() => {
                    window.location.reload();
                }, 1000);
            });
        });
    });
}

btnClose?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('translate-x-0');
    panel?.classList.add('translate-x-full');
    panel?.addEventListener('transitionend', modalToggle);
});

if (rewardId && rewardId.value) {
    http.get<ServerResponse<Reward>>(`${routers.reward.getOne}?rewardId=${rewardId.value}`).then(({ data }) => {
        const rewardImage = document.getElementById('reward-image') as HTMLImageElement;
        const rewardDescription = document.getElementById('reward-description') as HTMLParagraphElement;
        if (rewardImage && rewardDescription) {
            rewardImage.src = data.data.imageUrl;
            rewardDescription.innerText = data.data.description;
        }
    });
    rewardId.addEventListener('change', function () {
        http.get<ServerResponse<Reward>>(`${routers.reward.getOne}?rewardId=${rewardId.value}`).then(({ data }) => {
            const rewardImage = document.getElementById('reward-image') as HTMLImageElement;
            const rewardDescription = document.getElementById('reward-description') as HTMLParagraphElement;
            if (rewardImage && rewardDescription) {
                rewardImage.src = data.data.imageUrl;
                rewardDescription.innerText = data.data.description;
            }
        });
    });
}
const buildChart = () => {
    const viewChart = document.getElementById('view-chart');
    const label = viewChart?.getElementsByTagName('span')[0] as HTMLSpanElement;
    const chartElement = document.getElementById('chart');
    let chart: ApexCharts;
    let isClick = false;

    viewChart?.addEventListener('click', function () {
        if (!isClick) {
            http.get<ServerResponse<{ name: string; total: number }[]>>(routers.reward.chart).then(({ data }) => {
                const names = data.data.map((item) => item.name);
                const totals = data.data.map((item) => item.total);

                const options: ApexOptions = {
                    series: totals,
                    chart: {
                        width: 380,
                        type: 'pie',
                        toolbar: {
                            show: true,
                        },
                    },
                    title: {
                        text: 'Reward of user',
                        align: 'center',
                    },
                    labels: names,
                    tooltip: {
                        enabled: true,
                    },
                    responsive: [
                        {
                            breakpoint: 480,
                            options: {
                                chart: {
                                    width: 200,
                                },
                                legend: {
                                    position: 'bottom',
                                },
                            },
                        },
                    ],
                };
                isClick = true;
                chart = new ApexCharts(chartElement, options);
                label.innerText = 'Close Chart';
                chart.render();
            });
        } else {
            isClick = false;
            label.innerText = 'View Chart';
            chart.destroy();
        }
    });
};
buildChart();
