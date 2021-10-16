import { ApexOptions } from 'apexcharts';
import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { pageChange } from '../package/helper/pagination';
import { ServerResponse } from '../package/interface/serverResponse';
pageChange('listTagForm');

const clearTagBtn = document.getElementById('clear-unused-tag');

clearTagBtn?.addEventListener('click', function () {
    http.put(routers.tag.clearUnused).then(() => {
        setTimeout(() => {
            window.location.reload();
        }, 1000);
    });
});

const buildChart = () => {
    const viewChart = document.getElementById('view-chart');
    const label = viewChart?.getElementsByTagName('span')[0] as HTMLSpanElement;
    const chartElement = document.getElementById('chart');
    let chart: ApexCharts;
    let isClick = false;

    viewChart?.addEventListener('click', function () {
        if (!isClick) {
            http.get<ServerResponse<{ name: string; total: number }[]>>(routers.tag.chart).then(({ data }) => {
                const names = data.data.map((item) => item.name);
                const totals = data.data.map((item) => item.total);

                const options: ApexOptions = {
                    series: totals,
                    chart: {
                        width: 380,
                        type: 'pie',
                    },
                    title: {
                        text: 'Top 10 trending tag',
                        align: 'center',
                    },
                    labels: names,
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
