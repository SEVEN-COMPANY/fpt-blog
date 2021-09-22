"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("../package/axios");
var routes_1 = require("../package/axios/routes");
var status = 1;
var createCategoryForm = document.getElementById('updateCategoryForm');
var statusList = document.querySelectorAll('input[name="status"]');
statusList.forEach(function (radio) {
    var element = radio;
    if (element.getAttribute('checked')) {
        status = Number(element.value);
    }
    radio.addEventListener('click', function () {
        status = Number(element.value);
    });
});
createCategoryForm === null || createCategoryForm === void 0 ? void 0 : createCategoryForm.addEventListener('submit', function (event) {
    event.preventDefault();
    var name = document.getElementById('name');
    var description = document.getElementById('description');
    var categoryId = document.getElementById('categoryId');
    if (name != null && description != null && status != null) {
        var input = {
            name: name.value,
            description: description.value,
            status: status,
            categoryId: categoryId.value,
        };
        axios_1.http.post(routes_1.routers.category.update, input);
    }
});
