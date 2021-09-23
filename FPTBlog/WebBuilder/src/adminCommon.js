"use strict";
var _loop_1 = function (index) {
    var btn = document.getElementById("menu-btn-" + index);
    var container = document.getElementById("menu-container-" + index);
    var menu = document.getElementById("menu-list-" + index);
    var arrow = document.getElementById("menu-arrow-" + index);
    if (btn && menu && arrow && container) {
        btn.addEventListener('click', function () {
            console.log('hello');
            menu.classList.toggle('scale-y-0');
            btn.setAttribute('disabled', 'disabled');
            if (container.clientHeight <= menu.clientHeight) {
                container.style.height = container.clientHeight + menu.clientHeight + 4 + 'px';
            }
            else {
                container.style.height = '40px';
            }
            arrow.classList.toggle('rotate-180');
        }, false);
    }
};
for (var index = 0; index <= 4; index++) {
    _loop_1(index);
}
