(()=>{"use strict";var e={284:(e,t,n)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var o=n(603),a=axios;t.http=a,a.defaults.withCredentials=!0,a.interceptors.request.use(o.requestInterceptor),a.interceptors.response.use(o.responseSuccessInterceptor,o.responseFailedInterceptor)},603:(e,t,n)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0;var o=n(973);function a(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),n=document.getElementById("loading"),o=document.getElementById("MESSAGEERROR"),a=document.getElementById("ERRORMESSAGEERROR");for(var s in e.data){var i=document.getElementById(s.toUpperCase()+"ERROR");i&&(i.innerHTML="")}return a&&(a.innerHTML=""),o&&(o.innerHTML=""),n&&t&&(t.classList.add("hidden"),n.classList.remove("hidden"),n.classList.add("flex")),e},t.handleCommonResponse=a,t.responseSuccessInterceptor=function(e){var t,n,s,i,d,r;if(a(),null===(n=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===n?void 0:n.message){var l=document.getElementById("MESSAGEERROR");l&&(l.innerHTML=null===(i=null===(s=null==e?void 0:e.data)||void 0===s?void 0:s.details)||void 0===i?void 0:i.message),document.getElementById("toastify")&&(0,o.toastify)({text:null===(r=null===(d=null==e?void 0:e.data)||void 0===d?void 0:d.details)||void 0===r?void 0:r.message,duration:2e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"#F37124",stopOnFocus:!0})}return e},t.responseFailedInterceptor=function(e){var t,n;if(a(),null===(n=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===n?void 0:n.details){var s=e.response.data.details;for(var i in s){var d=document.getElementById(i.toUpperCase()+"ERROR");d&&(d.innerHTML=d.getAttribute("data-label")+" "+s[i]),!d||"errorMessage"!==i&&"message"!==i||(d.innerHTML=""+s[i],document.getElementById("toastify")&&(0,o.toastify)({text:""+s[i],duration:2e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"rgb(239, 68, 68)",stopOnFocus:!0}))}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category"},post:{create:"/api/post",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike",sendPost:"/api/post/send",approvedPost:"/api/admin/post/approved",deletePost:"/api/post/delete"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",status:"/api/admin/user/status",role:"/api/admin/user/role"},tag:{getAll:"/api/tag/all",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"}}},623:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.pageChange=void 0,t.pageChange=function(e){var t=document.getElementById("pagination-size"),n=document.getElementById("pagination-btn");null==t||t.addEventListener("change",(function(n){var o=t.options[t.selectedIndex];document.getElementById("pageSize").value=o.value,document.getElementById("pageIndex").value="0",document.getElementById(e).submit()}));var o=null==n?void 0:n.getElementsByTagName("button");if(o)for(var a=function(t){var n=o[t];n.addEventListener("click",(function(t){var o=document.getElementById("pageIndex"),a=n.getAttribute("data-index");a&&(o.value=a),document.getElementById(e).submit()}))},s=0;s<o.length;s++)a(s)}},973:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.toastify=void 0,t.toastify=function(e){return Toastify(e).showToast()}}},t={};function n(o){var a=t[o];if(void 0!==a)return a.exports;var s=t[o]={exports:{}};return e[o](s,s.exports,n),s.exports}(()=>{var e=n(284),t=n(312);(0,n(623).pageChange)("listPostForm");for(var o=document.getElementsByClassName("modal-btn"),a=document.getElementById("modal-btn-close"),s=document.getElementById("modal-wrapper"),i=document.getElementById("modal-bg"),d=document.getElementById("modal-panel"),r=function(){null==s||s.classList.add("invisible")},l=function(n){var a=o[n];null==a||a.addEventListener("click",(function(){null==s||s.classList.remove("invisible"),null==i||i.classList.add("opacity-100"),null==i||i.classList.remove("opacity-0"),null==d||d.classList.add("translate-x-0"),null==d||d.classList.remove("translate-x-full"),null==d||d.removeEventListener("transitionend",r);var n=a.getAttribute("data-postId"),o=a.getAttribute("data-title"),l=document.getElementById("postName"),u=document.getElementById("postLink");if(o&&l&&(l.innerText=o),u&&n&&u.setAttribute("href","/post?postId="+n),n){var p=document.getElementById("approvedPostForm");null==p||p.addEventListener("submit",(function(o){var a=document.getElementById("approvedStatus"),s=document.getElementById("note");o.preventDefault();var i={note:s.value,status:a.value,postId:n};e.http.post(t.routers.post.approvedPost,i).then((function(){setTimeout((function(){window.location.reload()}),1e3)}))}))}}))},u=0;u<o.length;u++)l(u);null==a||a.addEventListener("click",(function(){null==i||i.classList.remove("opacity-100"),null==i||i.classList.add("opacity-0"),null==d||d.classList.remove("translate-x-0"),null==d||d.classList.add("translate-x-full"),null==d||d.addEventListener("transitionend",r)}))})()})();