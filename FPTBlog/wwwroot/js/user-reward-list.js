(()=>{"use strict";var e={284:(e,t,a)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var n=a(603),o=axios;t.http=o,o.defaults.withCredentials=!0,o.interceptors.request.use(n.requestInterceptor),o.interceptors.response.use(n.responseSuccessInterceptor,n.responseFailedInterceptor)},603:(e,t,a)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0;var n=a(973);function o(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),a=document.getElementById("loading"),n=document.getElementById("MESSAGEERROR"),o=document.getElementById("ERRORMESSAGEERROR");for(var r in e.data){var i=document.getElementById(r.toUpperCase()+"ERROR");i&&(i.innerHTML="")}return o&&(o.innerHTML=""),n&&(n.innerHTML=""),a&&t&&(t.classList.add("hidden"),a.classList.remove("hidden"),a.classList.add("flex")),e},t.handleCommonResponse=o,t.responseSuccessInterceptor=function(e){var t,a,r,i,s,d;if(o(),null===(a=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===a?void 0:a.message){var l=document.getElementById("MESSAGEERROR");if(l){var u=null===(i=null===(r=null==e?void 0:e.data)||void 0===r?void 0:r.details)||void 0===i?void 0:i.message;l.innerHTML=u.slice(0,1).toUpperCase()+u.slice(1,u.length)}document.getElementById("toastify")&&(u=null===(d=null===(s=null==e?void 0:e.data)||void 0===s?void 0:s.details)||void 0===d?void 0:d.message,(0,n.toastify)({text:u.slice(0,1).toUpperCase()+u.slice(1,u.length),duration:4e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"#4CAF50",avatar:"/images/check.svg",stopOnFocus:!0}))}return e},t.responseFailedInterceptor=function(e){var t,a;if(o(),null===(a=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===a?void 0:a.details){var r=e.response.data.details;for(var i in r){var s=document.getElementById(i.toUpperCase()+"ERROR");if(s&&(s.innerHTML=s.getAttribute("data-label")+" "+r[i]),s&&("errorMessage"===i||"message"===i)){var d=""+r[i];s.innerHTML=d.slice(0,1).toUpperCase()+d.slice(1,d.length),document.getElementById("toastify")&&(0,n.toastify)({text:d.slice(0,1).toUpperCase()+d.slice(1,d.length),duration:4e3,newWindow:!0,close:!0,gravity:"top",position:"right",avatar:"/images/minus.svg",backgroundColor:"#F44336",stopOnFocus:!0})}}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category",chart:"/api/category/chart"},post:{create:"/api/post",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike",sendPost:"/api/post/send",approvedPost:"/api/admin/post/approved",deletePost:"/api/post/delete"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",chart:"/api/admin/user/chart",status:"/api/admin/user/status",role:"/api/admin/user/role",follow:"/api/user/follow"},tag:{getAll:"/api/tag/all",chart:"/api/tag/chart",clearUnused:"/api/tag/unused",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"},reward:{create:"/api/reward",update:"/api/reward",getOne:"/api/reward",give:"/api/reward/give",delete:"/api/reward/delete",removeUserReward:"/api/reward/remove"},comment:{create:"/api/comment",getComment:"/api/comment/post",deleteComment:"/api/comment/delete",updateComment:"/api/comment",likeComment:"/api/comment/like",dislikeComment:"/api/comment/dislike"}}},623:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.pageChange=void 0,t.pageChange=function(e){var t=document.getElementById("pagination-size"),a=document.getElementById("pagination-btn");null==t||t.addEventListener("change",(function(a){var n=t.options[t.selectedIndex];document.getElementById("pageSize").value=n.value,document.getElementById("pageIndex").value="0",document.getElementById(e).submit()}));var n=null==a?void 0:a.getElementsByTagName("button");if(n)for(var o=function(t){var a=n[t];a.addEventListener("click",(function(t){var n=document.getElementById("pageIndex"),o=a.getAttribute("data-index");o&&(n.value=o),document.getElementById(e).submit()}))},r=0;r<n.length;r++)o(r)}},973:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.toastify=void 0,t.toastify=function(e){return Toastify(e).showToast()}}},t={};function a(n){var o=t[n];if(void 0!==o)return o.exports;var r=t[n]={exports:{}};return e[n](r,r.exports,a),r.exports}(()=>{var e=a(284),t=a(312);(0,a(623).pageChange)("listUserRewardForm");for(var n=document.getElementById("modal-btn-close"),o=document.getElementById("modal-wrapper"),r=document.getElementById("modal-bg"),i=document.getElementById("modal-panel"),s=document.getElementById("rewardId"),d=function(){null==o||o.classList.add("invisible")},l=document.getElementsByClassName("reward-btn"),u=function(a){var n=l[a];n.addEventListener("click",(function(){null==o||o.classList.remove("invisible"),null==r||r.classList.add("opacity-100"),null==r||r.classList.remove("opacity-0"),null==i||i.classList.add("translate-x-0"),null==i||i.classList.remove("translate-x-full"),null==i||i.removeEventListener("transitionend",d);var a=n.getAttribute("data-userid"),l=document.getElementById("giveRewardForm");null==l||l.addEventListener("submit",(function(n){n.preventDefault(),e.http.post(t.routers.reward.give,{userId:a,rewardId:s.value}).then((function(){setTimeout((function(){window.location.reload()}),1e3)}))}))}))},c=0;c<l.length;c++)u(c);null==n||n.addEventListener("click",(function(){null==r||r.classList.remove("opacity-100"),null==r||r.classList.add("opacity-0"),null==i||i.classList.remove("translate-x-0"),null==i||i.classList.add("translate-x-full"),null==i||i.addEventListener("transitionend",d)})),s&&s.value&&(e.http.get(t.routers.reward.getOne+"?rewardId="+s.value).then((function(e){var t=e.data,a=document.getElementById("reward-image"),n=document.getElementById("reward-description");a&&n&&(a.src=t.data.imageUrl,n.innerText=t.data.description)})),s.addEventListener("change",(function(){e.http.get(t.routers.reward.getOne+"?rewardId="+s.value).then((function(e){var t=e.data,a=document.getElementById("reward-image"),n=document.getElementById("reward-description");a&&n&&(a.src=t.data.imageUrl,n.innerText=t.data.description)}))})))})()})();