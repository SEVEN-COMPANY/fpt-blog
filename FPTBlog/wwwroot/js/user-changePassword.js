(()=>{"use strict";var e,t,a,o={284:(e,t,a)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var o=a(603),r=axios;t.http=r,r.defaults.withCredentials=!0,r.interceptors.request.use(o.requestInterceptor),r.interceptors.response.use(o.responseSuccessInterceptor,o.responseFailedInterceptor)},603:(e,t,a)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0;var o=a(973);function r(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),a=document.getElementById("loading"),o=document.getElementById("MESSAGEERROR"),r=document.getElementById("ERRORMESSAGEERROR");for(var s in e.data){var i=document.getElementById(s.toUpperCase()+"ERROR");i&&(i.innerHTML="")}return r&&(r.innerHTML=""),o&&(o.innerHTML=""),a&&t&&(t.classList.add("hidden"),a.classList.remove("hidden"),a.classList.add("flex")),e},t.handleCommonResponse=r,t.responseSuccessInterceptor=function(e){var t,a,s,i,n,d;if(r(),null===(a=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===a?void 0:a.message){var l=document.getElementById("MESSAGEERROR");if(l){var p=null===(i=null===(s=null==e?void 0:e.data)||void 0===s?void 0:s.details)||void 0===i?void 0:i.message;l.innerHTML=p.slice(0,1).toUpperCase()+p.slice(1,p.length)}document.getElementById("toastify")&&(p=null===(d=null===(n=null==e?void 0:e.data)||void 0===n?void 0:n.details)||void 0===d?void 0:d.message,(0,o.toastify)({text:p.slice(0,1).toUpperCase()+p.slice(1,p.length),duration:4e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"#4CAF50",avatar:"/images/check.svg",stopOnFocus:!0}))}return e},t.responseFailedInterceptor=function(e){var t,a;if(r(),null===(a=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===a?void 0:a.details){var s=e.response.data.details;for(var i in s){var n=document.getElementById(i.toUpperCase()+"ERROR"),d=document.getElementById("toastify");if(n&&(n.innerHTML=n.getAttribute("data-label")+" "+s[i]),n&&("errorMessage"===i||"message"===i)){var l=""+s[i];n.innerHTML=l.slice(0,1).toUpperCase()+l.slice(1,l.length)}!d||"errorMessage"!==i&&"message"!==i||(l=""+s[i],(0,o.toastify)({text:l.slice(0,1).toUpperCase()+l.slice(1,l.length),duration:4e3,newWindow:!0,close:!0,gravity:"top",position:"right",avatar:"/images/minus.svg",backgroundColor:"#F44336",stopOnFocus:!0}))}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category",chart:"/api/category/chart"},post:{create:"/api/post",chart:"/api/admin/post/chart",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike",sendPost:"/api/post/send",approvedPost:"/api/admin/post/approved",deletePost:"/api/post/delete"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",chart:"/api/admin/user/chart",status:"/api/admin/user/status",role:"/api/admin/user/role",follow:"/api/user/follow"},tag:{getAll:"/api/tag/all",chart:"/api/tag/chart",clearUnused:"/api/tag/unused",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"},reward:{create:"/api/reward",update:"/api/reward",getOne:"/api/reward",chart:"/api/reward/chart",give:"/api/reward/give",delete:"/api/reward/delete",removeUserReward:"/api/reward/remove"},comment:{create:"/api/comment",getComment:"/api/comment/post",deleteComment:"/api/comment/delete",updateComment:"/api/comment",likeComment:"/api/comment/like",dislikeComment:"/api/comment/dislike"},notification:{create:"/api/admin/notification",get:"/api/admin/notification"}}},973:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.toastify=void 0,t.toastify=function(e){return Toastify(e).showToast()}}},r={};function s(e){var t=r[e];if(void 0!==t)return t.exports;var a=r[e]={exports:{}};return o[e](a,a.exports,s),a.exports}e=s(284),t=s(312),null==(a=document.getElementById("changeUserPasswordForm"))||a.addEventListener("submit",(function(a){a.preventDefault();var o=document.getElementById("oldPassword"),r=document.getElementById("newPassword"),s=document.getElementById("confirmNewPassword");if(null!==o&&null!==r&&null!==s){var i={oldPassword:o.value,newPassword:r.value,confirmNewPassword:s.value};e.http.post(t.routers.user.changePassword,i)}}))})();