import { pageChange } from '../package/helper/pagination';
import { slideOver } from '../package/modal/index';
import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';
import { ApexOptions } from 'apexcharts';
pageChange('listCategoryForm');
slideOver('modal');
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
            setTimeout(() => {
                window.location.reload();
            }, 700);
        });
    }
});

const buildChart = () => {
    const viewChart = document.getElementById('view-chart');
    const label = viewChart?.getElementsByTagName('span')[0] as HTMLSpanElement;
    const chartElement = document.getElementById('chart');
    let chart: ApexCharts;
    let isClick = false;

    viewChart?.addEventListener('click', function () {
        if (!isClick) {
            http.get<ServerResponse<{ name: string; total: number }[]>>(routers.category.chart).then(({ data }) => {
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
                        text: 'Category and post chart',
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
