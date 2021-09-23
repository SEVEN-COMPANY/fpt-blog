"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("../package/axios");
var routes_1 = require("../package/axios/routes");
var updateUserForm = document.getElementById('updateUserForm');
updateUserForm === null || updateUserForm === void 0 ? void 0 : updateUserForm.addEventListener('submit', function (event) {
    event.preventDefault();
    var name = document.getElementById('name');
    var email = document.getElementById('email');
    var address = document.getElementById('address');
    var phone = document.getElementById('phone');
    if (name !== null && email !== null && address !== null && phone !== null) {
        var input = {
            name: name.value,
            email: email.value,
            address: address.value,
            phone: phone.value,
        };
        axios_1.http.put(routes_1.routers.user.update, input);
    }
});
