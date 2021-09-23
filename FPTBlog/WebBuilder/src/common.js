"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var routes_1 = require("./package/axios/routes");
var axios_1 = require("./package/axios");
var getCurrentUser = function () {
    axios_1.http.get(routes_1.routers.getUser).then(function (res) {
        var name = document.getElementById('user-name');
        var username = document.getElementById('user-username');
        var userAvatar = document.getElementById('user-avatar');
        if (name !== null && userAvatar !== null && username !== null) {
            name.innerHTML = res.data.data.name;
            username.innerHTML = res.data.data.username;
            userAvatar.src = res.data.data.avatarUrl;
        }
        // if (createDate) {
        //     createDate.classList.add('hidden');
        //     createDate.classList.remove('md:flex');
        // }
    });
};
getCurrentUser();
