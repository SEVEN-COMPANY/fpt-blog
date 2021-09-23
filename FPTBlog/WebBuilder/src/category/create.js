"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("../package/axios");
var routes_1 = require("../package/axios/routes");
var status = 1;
var createCategoryForm = document.getElementById('createCategoryForm');
var statusList = document.querySelectorAll('input[name="status"]');
statusList.forEach(function (radio) {
    radio.addEventListener('click', function () {
        status = Number(radio.value);
    });
});
createCategoryForm === null || createCategoryForm === void 0 ? void 0 : createCategoryForm.addEventListener('submit', function (event) {
    event.preventDefault();
    var name = document.getElementById('name');
    var description = document.getElementById('description');
    if (name != null && description != null && status != null) {
        var input = {
            name: name.value,
            description: description.value,
            status: status,
        };
        axios_1.http.post(routes_1.routers.category.create, input).then(function () {
            name.value = '';
            description.value = '';
        });
    }
});
