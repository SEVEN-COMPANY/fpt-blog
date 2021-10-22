(()=>{"use strict";var e={284:(e,t,n)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.http=void 0;var a=n(603),s=axios;t.http=s,s.defaults.withCredentials=!0,s.interceptors.request.use(a.requestInterceptor),s.interceptors.response.use(a.responseSuccessInterceptor,a.responseFailedInterceptor)},603:(e,t,n)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.responseFailedInterceptor=t.responseSuccessInterceptor=t.handleCommonResponse=t.requestInterceptor=void 0;var a=n(973);function s(){var e=document.getElementById("form-btn"),t=document.getElementById("loading");t&&e&&(e.classList.remove("hidden"),e.classList.add("block"),t.classList.add("hidden"),t.classList.remove("flex"))}t.requestInterceptor=function(e){var t=document.getElementById("form-btn"),n=document.getElementById("loading"),a=document.getElementById("MESSAGEERROR"),s=document.getElementById("ERRORMESSAGEERROR");for(var l in e.data){var i=document.getElementById(l.toUpperCase()+"ERROR");i&&(i.innerHTML="")}return s&&(s.innerHTML=""),a&&(a.innerHTML=""),n&&t&&(t.classList.add("hidden"),n.classList.remove("hidden"),n.classList.add("flex")),e},t.handleCommonResponse=s,t.responseSuccessInterceptor=function(e){var t,n,l,i,o,d;if(s(),null===(n=null===(t=null==e?void 0:e.data)||void 0===t?void 0:t.details)||void 0===n?void 0:n.message){var r=document.getElementById("MESSAGEERROR");if(r){var c=null===(i=null===(l=null==e?void 0:e.data)||void 0===l?void 0:l.details)||void 0===i?void 0:i.message;r.innerHTML=c.slice(0,1).toUpperCase()+c.slice(1,c.length)}document.getElementById("toastify")&&(c=null===(d=null===(o=null==e?void 0:e.data)||void 0===o?void 0:o.details)||void 0===d?void 0:d.message,(0,a.toastify)({text:c.slice(0,1).toUpperCase()+c.slice(1,c.length),duration:4e3,newWindow:!0,close:!0,gravity:"top",position:"right",backgroundColor:"#4CAF50",avatar:"/images/check.svg",stopOnFocus:!0}))}return e},t.responseFailedInterceptor=function(e){var t,n;if(s(),null===(n=null===(t=e.response)||void 0===t?void 0:t.data)||void 0===n?void 0:n.details){var l=e.response.data.details;for(var i in l){var o=document.getElementById(i.toUpperCase()+"ERROR"),d=document.getElementById("toastify");if(o&&(o.innerHTML=o.getAttribute("data-label")+" "+l[i]),o&&("errorMessage"===i||"message"===i)){var r=""+l[i];o.innerHTML=r.slice(0,1).toUpperCase()+r.slice(1,r.length)}!d||"errorMessage"!==i&&"message"!==i||(r=""+l[i],(0,a.toastify)({text:r.slice(0,1).toUpperCase()+r.slice(1,r.length),duration:4e3,newWindow:!0,close:!0,gravity:"top",position:"right",avatar:"/images/minus.svg",backgroundColor:"#F44336",stopOnFocus:!0}))}}return Promise.reject(e.response)}},312:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.routers=t.routerLinks=void 0,t.routerLinks={home:"/",loginForm:"/auth/login"},t.routers={category:{create:"/api/category",update:"/api/category",chart:"/api/category/chart"},post:{create:"/api/post",chart:"/api/admin/post/chart",addNewTagToPost:"/api/post/tag",getTagOfPost:function(e){return"/api/post/tag?postId="+e},save:"/api/post/save",uploadImagePost:"/api/post/image",addCategoryToPost:"/api/post/category",likePost:"/api/post/like",dislikePost:"/api/post/dislike",sendPost:"/api/post/send",approvedPost:"/api/admin/post/approved",deletePost:"/api/post/delete"},user:{changePassword:"/api/user/change-password",update:"/api/user",get:"/api/user",chart:"/api/admin/user/chart",status:"/api/admin/user/status",role:"/api/admin/user/role",follow:"/api/user/follow"},tag:{getAll:"/api/tag/all",chart:"/api/tag/chart",clearUnused:"/api/tag/unused",getByName:function(e){return"/api/tag?name="+e}},auth:{login:"/api/auth/login",register:"/api/auth/register"},reward:{create:"/api/reward",update:"/api/reward",getOne:"/api/reward",chart:"/api/reward/chart",give:"/api/reward/give",delete:"/api/reward/delete",removeUserReward:"/api/reward/remove"},comment:{create:"/api/comment",getComment:"/api/comment/post",deleteComment:"/api/comment/delete",updateComment:"/api/comment",likeComment:"/api/comment/like",dislikeComment:"/api/comment/dislike"},notification:{create:"/api/admin/notification",get:"/api/admin/notification"}}},623:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.pageChange=void 0,t.pageChange=function(e){var t=document.getElementById("pagination-size"),n=document.getElementById("pagination-btn");null==t||t.addEventListener("change",(function(n){var a=t.options[t.selectedIndex];document.getElementById("pageSize").value=a.value,document.getElementById("pageIndex").value="0",document.getElementById(e).submit()}));var a=null==n?void 0:n.getElementsByTagName("button");if(a)for(var s=function(t){var n=a[t];n.addEventListener("click",(function(t){var a=document.getElementById("pageIndex"),s=n.getAttribute("data-index");s&&(a.value=s),document.getElementById(e).submit()}))},l=0;l<a.length;l++)s(l)}},973:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.toastify=void 0,t.toastify=function(e){return Toastify(e).showToast()}}},t={};function n(a){var s=t[a];if(void 0!==s)return s.exports;var l=t[a]={exports:{}};return e[a](l,l.exports,n),l.exports}(()=>{var e,t,a,s=n(623),l=n(284),i=n(312),o=document.getElementById("modal-btn-close"),d=document.getElementById("modal-wrapper"),r=document.getElementById("modal-bg"),c=document.getElementById("modal-panel"),u=document.getElementById("modal-content-title-block"),m=document.getElementById("modal-content-title-unblock"),p=document.getElementById("modal-content-description-block"),v=document.getElementById("modal-content-description-unblock"),g=document.getElementById("modal-btn-accept-block"),y=document.getElementById("modal-btn-accept-unblock"),L=document.getElementById("modal-btn-cancel");!function(e){e[e.INFORMATION=1]="INFORMATION",e[e.WARNING=2]="WARNING",e[e.BANED=3]="BANED"}(e||(e={})),(0,s.pageChange)("listUserForm"),function(e){e.STUDENT="STUDENT",e.LECTURER="LECTURER"}(t||(t={})),function(e){e.DISABLE="DISABLE",e.ENABLE="ENABLE"}(a||(a={}));for(var E=document.getElementsByTagName("tr"),h=null,I=a.ENABLE,f=t.STUDENT,B=function(){null==d||d.classList.add("invisible"),I==a.ENABLE&&(null==u||u.classList.add("hidden"),null==p||p.classList.add("hidden"),null==g||g.classList.add("hidden")),I==a.DISABLE&&(null==m||m.classList.add("hidden"),null==v||v.classList.add("hidden"),null==y||y.classList.add("hidden"))},b=function(e){var t=E[e].getElementsByClassName("modal-btn")[0];t&&t.addEventListener("click",(function(){t.getAttribute("data-userStatus")==a.ENABLE&&(I=a.ENABLE,null==u||u.classList.remove("hidden"),null==p||p.classList.remove("hidden"),null==g||g.classList.remove("hidden")),t.getAttribute("data-userStatus")==a.DISABLE&&(I=a.DISABLE,null==m||m.classList.remove("hidden"),null==v||v.classList.remove("hidden"),null==y||y.classList.remove("hidden")),null==d||d.classList.remove("invisible"),null==r||r.classList.add("opacity-100"),null==r||r.classList.remove("opacity-0"),null==c||c.classList.add("opacity-100","translate-y-0","sm:scale-100"),null==c||c.classList.remove("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==c||c.removeEventListener("transitionend",B),h=t.getAttribute("data-userId")}))},w=0;w<E.length;w++)b(w);null==g||g.addEventListener("click",(function(){if(null!==h){var t={userId:h},n=document.getElementById("content"),a=document.getElementById("description"),s={receiverId:h,level:e.BANED,content:n.value,description:a.value};l.http.post(i.routers.notification.create,s).then((function(){l.http.put(i.routers.user.status,t).then((function(){null==r||r.classList.remove("opacity-100"),null==r||r.classList.add("opacity-0"),null==c||c.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==c||c.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==c||c.addEventListener("transitionend",B),setTimeout((function(){window.location.reload()}),1e3)}))}))}})),null==y||y.addEventListener("click",(function(){if(null!==h){var t={userId:h},n=document.getElementById("content"),a=document.getElementById("description"),s={receiverId:h,level:e.INFORMATION,content:n.value,description:a.value};l.http.post(i.routers.notification.create,s).then((function(){l.http.put(i.routers.user.status,t).then((function(){null==r||r.classList.remove("opacity-100"),null==r||r.classList.add("opacity-0"),null==c||c.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==c||c.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==c||c.addEventListener("transitionend",B),setTimeout((function(){window.location.reload()}),1e3)}))}))}})),null==o||o.addEventListener("click",(function(){null==r||r.classList.remove("opacity-100"),null==r||r.classList.add("opacity-0"),null==c||c.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==c||c.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==c||c.addEventListener("transitionend",B)})),null==L||L.addEventListener("click",(function(){null==r||r.classList.remove("opacity-100"),null==r||r.classList.add("opacity-0"),null==c||c.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==c||c.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==c||c.addEventListener("transitionend",B)}));var T,R,A,k,C,N=document.getElementById("modal-role-btn-close"),S=document.getElementById("modal-role-wrapper"),O=document.getElementById("modal-role-bg"),U=document.getElementById("modal-role-panel"),M=document.getElementById("modal-content-title-upgrade"),x=document.getElementById("modal-content-title-downgrade"),P=document.getElementById("modal-content-description-upgrade"),D=document.getElementById("modal-content-description-downgrade"),F=document.getElementById("modal-role-btn-accept-upgrade"),_=document.getElementById("modal-role-btn-accept-downgrade"),j=document.getElementById("modal-role-btn-cancel"),H=function(){null==S||S.classList.add("invisible"),f==t.STUDENT&&(I=a.ENABLE,null==M||M.classList.add("hidden"),null==P||P.classList.add("hidden"),null==F||F.classList.add("hidden")),f==t.LECTURER&&(I=a.DISABLE,null==x||x.classList.add("hidden"),null==D||D.classList.add("hidden"),null==_||_.classList.add("hidden"))},G=function(e){var n=E[e].getElementsByClassName("modal-role-btn")[0];n&&n.addEventListener("click",(function(){n.getAttribute("data-userRole")==t.STUDENT&&(f=t.STUDENT,null==M||M.classList.remove("hidden"),null==P||P.classList.remove("hidden"),null==F||F.classList.remove("hidden")),n.getAttribute("data-userRole")==t.LECTURER&&(f=t.LECTURER,null==x||x.classList.remove("hidden"),null==D||D.classList.remove("hidden"),null==_||_.classList.remove("hidden")),null==S||S.classList.remove("invisible"),null==O||O.classList.add("opacity-100"),null==O||O.classList.remove("opacity-0"),null==U||U.classList.add("opacity-100","translate-y-0","sm:scale-100"),null==U||U.classList.remove("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==U||U.removeEventListener("transitionend",H),h=n.getAttribute("data-userId")}))};for(w=0;w<E.length;w++)G(w);null==F||F.addEventListener("click",(function(){if(null==O||O.classList.remove("opacity-100"),null==O||O.classList.add("opacity-0"),null==U||U.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==U||U.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==U||U.addEventListener("transitionend",H),null!==h){var e={userId:h};l.http.put(i.routers.user.role,e).then((function(){window.location.reload()}))}})),null==_||_.addEventListener("click",(function(){if(null==O||O.classList.remove("opacity-100"),null==O||O.classList.add("opacity-0"),null==U||U.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==U||U.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==U||U.addEventListener("transitionend",H),null!==h){var e={userId:h};l.http.put(i.routers.user.role,e).then((function(){window.location.reload()}))}})),null==N||N.addEventListener("click",(function(){null==O||O.classList.remove("opacity-100"),null==O||O.classList.add("opacity-0"),null==U||U.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==U||U.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==U||U.addEventListener("transitionend",H)})),null==j||j.addEventListener("click",(function(){null==O||O.classList.remove("opacity-100"),null==O||O.classList.add("opacity-0"),null==U||U.classList.remove("opacity-100","translate-y-0","sm:scale-100"),null==U||U.classList.add("opacity-0","translate-y-4","sm:translate-y-0","sm:scale-95"),null==U||U.addEventListener("transitionend",H)})),R=document.getElementById("view-chart"),A=null==R?void 0:R.getElementsByTagName("span")[0],k=document.getElementById("chart"),C=!1,null==R||R.addEventListener("click",(function(){C?(C=!1,A.innerText="View Chart",T.destroy()):l.http.get(i.routers.user.chart).then((function(e){var t=e.data,n={series:[t.data.totalStudent,t.data.totalLecturer],chart:{width:380,type:"pie",toolbar:{show:!0}},title:{text:"User Chart",align:"right"},labels:["Student","Lecture"],responsive:[{breakpoint:480,options:{chart:{width:200},legend:{position:"bottom"}}}],tooltip:{enabled:!0},colors:["#f37124","#60a5fa"]};C=!0,T=new ApexCharts(k,n),A.innerText="Close Chart",T.render()}))}))})()})();