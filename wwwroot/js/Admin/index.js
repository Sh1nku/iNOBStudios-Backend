$(document).ready(function () {
    new Vue({
        el: '#app',
        data: {
            posts: null,

        },
        mounted: function () {
            this.$nextTick(function () {
                this.posts = model
            });
        }
    });
});