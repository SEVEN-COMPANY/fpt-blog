import { pageChange } from '../package/helper/pagination';
import { http } from '../package/axios';
import { routers } from '../package/axios/routes';
import { ServerResponse } from '../package/interface/serverResponse';
import { ApexOptions } from 'apexcharts';
// modal of user status
const btnClose = document.getElementById(`modal-btn-close`);
const wrapper = document.getElementById(`modal-wrapper`);
const bg = document.getElementById(`modal-bg`);
const panel = document.getElementById(`modal-panel`);
const contentTitleBlock = document.getElementById(`modal-content-title-block`);
const contentTitleUnblock = document.getElementById(`modal-content-title-unblock`);
const contentDescriptionBlock = document.getElementById(`modal-content-description-block`);
const contentDescriptionUnblock = document.getElementById(`modal-content-description-unblock`);
const btnAcceptBlock = document.getElementById(`modal-btn-accept-block`);
const btnAcceptUnblock = document.getElementById(`modal-btn-accept-unblock`);
const btnCancel = document.getElementById(`modal-btn-cancel`);

pageChange('listUserForm');

interface ToggleUserDto {
    userId: string;
}

enum UserRole {
    STUDENT = 'STUDENT',
    LECTURER = 'LECTURER',
}

enum UserStatus {
    DISABLE = 'DISABLE',
    ENABLE = 'ENABLE',
}

const rows = document.getElementsByTagName('tr');
let userId: any = null;
let userStatus = UserStatus.ENABLE;
let userRole = UserRole.STUDENT;

const modalToggle = () => {
    wrapper?.classList.add('invisible');
    if (userStatus == UserStatus.ENABLE) {
        contentTitleBlock?.classList.add('hidden');
        contentDescriptionBlock?.classList.add('hidden');
        btnAcceptBlock?.classList.add('hidden');
    }
    if (userStatus == UserStatus.DISABLE) {
        contentTitleUnblock?.classList.add('hidden');
        contentDescriptionUnblock?.classList.add('hidden');
        btnAcceptUnblock?.classList.add('hidden');
    }
};

for (let index = 0; index < rows.length; index++) {
    const element = rows[index] as HTMLTableRowElement;
    const btn = element.getElementsByClassName('modal-btn')[0] as HTMLButtonElement;
    if (btn)
        btn.addEventListener('click', function () {
            if (btn.getAttribute('data-userStatus') == UserStatus.ENABLE) {
                userStatus = UserStatus.ENABLE;
                contentTitleBlock?.classList.remove('hidden');
                contentDescriptionBlock?.classList.remove('hidden');
                btnAcceptBlock?.classList.remove('hidden');
            }
            if (btn.getAttribute('data-userStatus') == UserStatus.DISABLE) {
                userStatus = UserStatus.DISABLE;
                contentTitleUnblock?.classList.remove('hidden');
                contentDescriptionUnblock?.classList.remove('hidden');
                btnAcceptUnblock?.classList.remove('hidden');
            }
            wrapper?.classList.remove('invisible');
            bg?.classList.add('opacity-100');
            bg?.classList.remove('opacity-0');
            panel?.classList.add('opacity-100', 'translate-y-0', 'sm:scale-100');
            panel?.classList.remove('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
            panel?.removeEventListener('transitionend', modalToggle);

            userId = btn.getAttribute('data-userId');
        });
}

btnAcceptBlock?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);

    if (userId !== null) {
        const input: ToggleUserDto = {
            userId: userId,
        };

        http.put<ServerResponse<null>>(routers.user.status, input).then(() => {
            window.location.reload();
        });
    }
});

btnAcceptUnblock?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);

    if (userId !== null) {
        const input: ToggleUserDto = {
            userId: userId,
        };

        http.put<ServerResponse<null>>(routers.user.status, input).then(() => {
            window.location.reload();
        });
    }
});

btnClose?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);
});

btnCancel?.addEventListener('click', function () {
    bg?.classList.remove('opacity-100');
    bg?.classList.add('opacity-0');
    panel?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panel?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panel?.addEventListener('transitionend', modalToggle);
});

// modal of user role
const btnRoleClose = document.getElementById(`modal-role-btn-close`);
const wrapperRole = document.getElementById(`modal-role-wrapper`);
const bgRole = document.getElementById(`modal-role-bg`);
const panelRole = document.getElementById(`modal-role-panel`);
const contentTitleUpgrade = document.getElementById(`modal-content-title-upgrade`);
const contentTitleDowngrade = document.getElementById(`modal-content-title-downgrade`);
const contentDescriptionUpgrade = document.getElementById(`modal-content-description-upgrade`);
const contentDescriptionDowngrade = document.getElementById(`modal-content-description-downgrade`);
const btnRoleAcceptUpgrade = document.getElementById(`modal-role-btn-accept-upgrade`);
const btnRoleAcceptDowngrade = document.getElementById(`modal-role-btn-accept-downgrade`);
const btnRoleCancel = document.getElementById(`modal-role-btn-cancel`);

const modalRoleToggle = () => {
    wrapperRole?.classList.add('invisible');
    if (userRole == UserRole.STUDENT) {
        userStatus = UserStatus.ENABLE;
        contentTitleUpgrade?.classList.add('hidden');
        contentDescriptionUpgrade?.classList.add('hidden');
        btnRoleAcceptUpgrade?.classList.add('hidden');
    }
    if (userRole == UserRole.LECTURER) {
        userStatus = UserStatus.DISABLE;
        contentTitleDowngrade?.classList.add('hidden');
        contentDescriptionDowngrade?.classList.add('hidden');
        btnRoleAcceptDowngrade?.classList.add('hidden');
    }
};

for (let index = 0; index < rows.length; index++) {
    const element = rows[index] as HTMLTableRowElement;
    const btn = element.getElementsByClassName('modal-role-btn')[0] as HTMLButtonElement;
    if (btn)
        btn.addEventListener('click', function () {
            if (btn.getAttribute('data-userRole') == UserRole.STUDENT) {
                userRole = UserRole.STUDENT;
                contentTitleUpgrade?.classList.remove('hidden');
                contentDescriptionUpgrade?.classList.remove('hidden');
                btnRoleAcceptUpgrade?.classList.remove('hidden');
            }
            if (btn.getAttribute('data-userRole') == UserRole.LECTURER) {
                userRole = UserRole.LECTURER;
                contentTitleDowngrade?.classList.remove('hidden');
                contentDescriptionDowngrade?.classList.remove('hidden');
                btnRoleAcceptDowngrade?.classList.remove('hidden');
            }
            wrapperRole?.classList.remove('invisible');
            bgRole?.classList.add('opacity-100');
            bgRole?.classList.remove('opacity-0');
            panelRole?.classList.add('opacity-100', 'translate-y-0', 'sm:scale-100');
            panelRole?.classList.remove('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
            panelRole?.removeEventListener('transitionend', modalRoleToggle);

            userId = btn.getAttribute('data-userId');
        });
}

btnRoleAcceptUpgrade?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);

    if (userId !== null) {
        const input: ToggleUserDto = {
            userId: userId,
        };

        http.put<ServerResponse<null>>(routers.user.role, input).then(() => {
            window.location.reload();
        });
    }
});

btnRoleAcceptDowngrade?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);

    if (userId !== null) {
        const input: ToggleUserDto = {
            userId: userId,
        };

        http.put<ServerResponse<null>>(routers.user.role, input).then(() => {
            window.location.reload();
        });
    }
});

btnRoleClose?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);
});

btnRoleCancel?.addEventListener('click', function () {
    bgRole?.classList.remove('opacity-100');
    bgRole?.classList.add('opacity-0');
    panelRole?.classList.remove('opacity-100', 'translate-y-0', 'sm:scale-100');
    panelRole?.classList.add('opacity-0', 'translate-y-4', 'sm:translate-y-0', 'sm:scale-95');
    panelRole?.addEventListener('transitionend', modalRoleToggle);
});

const buildChart = () => {
    const viewChart = document.getElementById('view-chart');
    const label = viewChart?.getElementsByTagName('span')[0] as HTMLSpanElement;
    const chartElement = document.getElementById('chart');
    let chart: ApexCharts;
    let isClick = false;

    viewChart?.addEventListener('click', function () {
        if (!isClick) {
            http.get<ServerResponse<{ totalLecturer: number; totalStudent: number }>>(routers.user.chart).then(({ data }) => {
                const options: ApexOptions = {
                    series: [data.data.totalStudent, data.data.totalLecturer],
                    chart: {
                        width: 380,
                        type: 'pie',
                    },
                    title: {
                        text: 'User Chart',
                        align: 'right',
                    },
                    labels: ['Student', 'Lecture'],
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
                    colors: ['#f37124', '#60a5fa'],
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
