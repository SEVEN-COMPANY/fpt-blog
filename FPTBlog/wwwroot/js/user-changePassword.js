(()=>{"use strict";var e,t,s,r={284:(e,t,s)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var r=s(603),o=axios;t.http=o,o.defaults.withCredentials=!0,o.interceptors.request.use(r.requestInterceptor),o.interceptors.response.use(r.responseSuccessInterceptor,r.responseFailedInterceptor)},603:(e,t)=>{function s(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0,t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),s=document.getElementById("loading"),r=document.getElementById("MESSAGEERROR"),o=document.getElementById("ERRORMESSAGEERROR");for(var a in e.data){var n=document.getElementById(a.toUpperCase()+"ERROR");n&&(n.innerHTML="")}return o&&(o.innerHTML=""),r&&(r.innerHTML=""),s&&t&&(t.classList.add("hidden"),s.classList.remove("hidden"),s.classList.add("flex")),e},t.handleCommonResponse=s,t.responseSuccessInterceptor=function(e){var t,r,o,a;if(s(),null===(r=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===r?void 0:r.message){var n=document.getElementById("MESSAGEERROR");n&&(n.innerHTML=null===(a=null===(o=null==e?void 0:e.data)||void 0===o?void 0:o.details)||void 0===a?void 0:a.message)}return e},t.responseFailedInterceptor=function(e){var t,r;if(s(),null===(r=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===r?void 0:r.details){var o=e.response.data.details;for(var a in o){var n=document.getElementById(a.toUpperCase()+"ERROR");n&&(n.innerHTML=n.getAttribute("data-label")+" "+o[a]),!n||"errorMessage"!==a&&"message"!==a||(n.innerHTML=""+o[a])}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category"},post:{create:"/api/post",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",status:"/api/admin/user/status",role:"/api/admin/user/role"},tag:{getAll:"/api/tag/all",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"}}}},o={};function a(e){var t=o[e];if(void 0!==t)return t.exports;var s=o[e]={exports:{}};return r[e](s,s.exports,a),s.exports}e=a(284),t=a(312),null==(s=document.getElementById("changeUserPasswordForm"))||s.addEventListener("submit",(function(s){s.preventDefault();var r=document.getElementById("oldPassword"),o=document.getElementById("newPassword"),a=document.getElementById("confirmNewPassword");if(null!==r&&null!==o&&null!==a){var n={oldPassword:r.value,newPassword:o.value,confirmNewPassword:a.value};e.http.post(t.routers.user.changePassword,n)}}))})();